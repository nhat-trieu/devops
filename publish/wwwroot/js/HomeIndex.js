import {
    request,
    giamGiaBan,
    muaNgay,
    themGH,
    dangXuat,
    isLoggedIn
} from './common.js';

var listCatLevel0 = [];
var listProduct = [];
var dictionaryCatLevel1 = {};
var dictionaryCatLevel2 = {};
var dictionaryCat_SanPham = {};

$(document).ready(function () {
    getListCatLevel0();
    check_BtDN();
});

// ========== Đăng nhập / Đăng xuất ==========
function check_BtDN() {
    const sdt = sessionStorage.getItem("sdt");
    const html = sdt
        ? `<p class="psdt">${sdt}</p>`
        : `<p class="psdt"></p>`;
    $("#sdtDN").html(html);
    $("#btdn").toggle(!sdt);
    $("#btdx").toggle(!!sdt);
}

// ========== Load danh mục ==========
function getListCatLevel0() {
    request("/Home/ListCat", 'GET', { parentId: null }, (response) => {
        listCatLevel0 = JSON.parse(response.data);
        let html = "";
        listCatLevel0.forEach(cat => {
            html += `<li class="menu0_item" onmouseover="onCatHoverIn(${cat.MaCate})">${cat.TenDanhMuc}</li>`;
        });
        $("#menu_level0").html(html);
        getListCatLevel1();
    });
}

function getListCatLevel1() {
    const promises = listCatLevel0.map(cat =>
        new Promise(done => {
            request("/Home/ListCat", 'GET', { parentId: cat.MaCate }, (response) => {
                dictionaryCatLevel1[cat.MaCate] = JSON.parse(response.data);
                done();
            });
        })
    );
    Promise.all(promises).then(() => {
        fillMenuChildForFirstCat0();
        getListCatLevel2();
    });
}

function getListCatLevel2() {
    const promises = [];
    listCatLevel0.forEach(cat0 => {
        const listLevel1 = dictionaryCatLevel1[cat0.MaCate];
        listLevel1.forEach(cat1 => {
            promises.push(new Promise(done => {
                request("/Home/ListCat", 'GET', { parentId: cat1.MaCate }, (response) => {
                    dictionaryCatLevel2[cat1.MaCate] = JSON.parse(response.data);
                    done();
                });
            }));
        });
    });
    Promise.all(promises).then(() => {
        getListProduct();
        fillMenuChildForCat1();
    });
}

function fillMenuChildForFirstCat0() {
    const cat0 = listCatLevel0[0];
    const cat0Childs = dictionaryCatLevel1[cat0.MaCate];
    if (!cat0Childs) return;
    let html = "";
    cat0Childs.forEach(cat1 => {
        html += `
        <div class="menuchild_item">
            <h4>${cat1.TenDanhMuc}</h4>
            <ul id="menucua${cat1.MaCate}" class="ul_munuchild"></ul>
        </div>`;
    });
    $("#menuchild").html(html);
}

function fillMenuChildForCat1() {
    const listLevel1 = dictionaryCatLevel1[listCatLevel0[0].MaCate];
    listLevel1.forEach(cat1 => {
        const listLevel2 = dictionaryCatLevel2[cat1.MaCate] || [];
        let html = "";
        listLevel2.forEach(cat2 => {
            html += `<a onclick="product_Category(${cat2.MaCate}, '${cat2.TenDanhMuc}')">${cat2.TenDanhMuc}</a><br/>`;
        });
        $(`#menucua${cat1.MaCate}`).html(html);
    });
}

// ========== Hover Danh Mục ==========
function onMenuHoverIn() {
    $("#row").show();
    fillMenuChildForFirstCat0();
    fillMenuChildForCat1();
}

function onMenuHoverOut() {
    $("#row").hide();
}

function onCatHoverIn(catId) {
    const listLevel1 = dictionaryCatLevel1[catId];
    let html = "";
    listLevel1.forEach(cat1 => {
        html += `
        <div class="menuchild_item">
            <h4>${cat1.TenDanhMuc}</h4>
            <ul id="menucua${cat1.MaCate}" class="ul_munuchild"></ul>
        </div>`;
    });
    $("#menuchild").html(html);

    listLevel1.forEach(cat1 => {
        const listLevel2 = dictionaryCatLevel2[cat1.MaCate] || [];
        let html2 = "";
        listLevel2.forEach(cat2 => {
            html2 += `<a onclick="product_Category(${cat2.MaCate}, '${cat2.TenDanhMuc}')">${cat2.TenDanhMuc}</a><br/>`;
        });
        $(`#menucua${cat1.MaCate}`).html(html2);
    });
}

// ========== Load sản phẩm ==========
function getListProduct() {
    const promises = [];
    listCatLevel0.forEach(cat0 => {
        const listLevel1 = dictionaryCatLevel1[cat0.MaCate];
        listLevel1.forEach(cat1 => {
            const listLevel2 = dictionaryCatLevel2[cat1.MaCate];
            listLevel2.forEach(cat2 => {
                promises.push(new Promise(done => {
                    request("/Home/ListProduct", 'GET', { maCatSach: cat2.MaCate }, (response) => {
                        dictionaryCat_SanPham[cat2.MaCate] = JSON.parse(response.data);
                        done();
                    });
                }));
            });
        });
    });

    Promise.all(promises).then(() => fillProduct());
}

function fillProduct() {
    let html = "";
    listCatLevel0.forEach(cat0 => {
        const listLevel1 = dictionaryCatLevel1[cat0.MaCate];
        listLevel1.forEach(cat1 => {
            const listLevel2 = dictionaryCatLevel2[cat1.MaCate];
            listLevel2.forEach(cat2 => {
                const products = dictionaryCat_SanPham[cat2.MaCate] || [];
                if (products.length > 0) {
                    html += `<div id="catPr${cat2.MaCate}" class="catProduct"></div>`;
                }
            });
        });
    });
    $("#listPr").html(html);

    listCatLevel0.forEach(cat0 => {
        const listLevel1 = dictionaryCatLevel1[cat0.MaCate];
        listLevel1.forEach(cat1 => {
            const listLevel2 = dictionaryCatLevel2[cat1.MaCate];
            listLevel2.forEach(cat2 => {
                const products = dictionaryCat_SanPham[cat2.MaCate] || [];
                if (products.length > 0) {
                    let html1 = `<label>${cat2.TenDanhMuc}</label><br/>`;
                    products.forEach(book => {
                        const giaGiam = giamGiaBan(book);
                        html1 += `
                            <div class="product">
                                <div class="img" onclick="chiTietSach(${book.MaSach})">
                                    <img src="${book.HinhAnh}" class="imgSach" />
                                </div>
                                <div class="ifSach">
                                    <p class="pSach">${book.TenSach}</p>
                                    <p class="pSach">${giaGiam} đ</p>
                                    <p class="pSach"><del>${book.GiaBan} đ</del></p>
                                    <input type="button" value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                                    <i class="fa fa-shopping-cart" onclick="themGH(${book.MaSach})"></i>
                                </div>
                            </div>`;
                    });
                    $(`#catPr${cat2.MaCate}`).html(html1);
                }
            });
        });
    });
}

function product_Category(catID, tenDM) {
    const products = dictionaryCat_SanPham[catID] || [];
    $("#listPr").html(`<div id="onlycatPr${catID}" class="catProduct"></div>`);
    let html = `<label>${tenDM}</label><br/>`;
    products.forEach(book => {
        const giaGiam = giamGiaBan(book);
        html += `
            <div class="product">
                <div class="img" onclick="chiTietSach(${book.MaSach})">
                    <img src="${book.HinhAnh}" class="imgSach" />
                </div>
                <div class="ifSach">
                    <p class="pSach">${book.TenSach}</p>
                    <p class="pSach">${giaGiam} đ</p>
                    <p class="pSach"><del>${book.GiaBan} đ</del></p>
                    <input type="button" value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                    <i class="fa fa-shopping-cart" onclick="themGH(${book.MaSach})"></i>
                </div>
            </div>`;
    });
    $(`#onlycatPr${catID}`).html(html);
}

var chitietsach = [];

function chiTietSach(maSach) {
    request("/Home/chiTietSach", 'GET', { maSach }, (response) => {
        chitietsach = JSON.parse(response.data);
        fillChiTiet();
    });
}

function fillChiTiet() {
    const book = chitietsach[0];
    const giaGiam = giamGiaBan(book);
    $("#listPr").html(`<div id="catPr${book.MaCate_SACH}" class="catProduct"></div>`);
    const html = `
        <div class="productchitiet">
            <div class="img"><img src="${book.HinhAnh}" class="imgSach" /></div>
            <div class="ifSach">
                <p class="pSach">${book.TenSach}</p>
                <p class="pSach">${giaGiam} đ</p>
                <p class="pSach"><del>${book.GiaBan} đ</del></p>
                <input type="button" value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                <i class="fa fa-shopping-cart" onclick="themGH(${book.MaSach})"></i>
            </div>
        </div>
        <div class="productchitiet">
            <div><p class="pTenchitiet">${book.TenSach}</p></div>
            <div class="chitiet">
                <div class="motachitiet">
                    <h5 class="h5title">Tác giả:</h5><h5>${book.TenTacGia}</h5><br/>
                    <h5 class="h5title">NXB:</h5><h5>${book.TenNXB}</h5><br/>
                    <h5 class="h5title">NCC:</h5><h5>${book.TenNCC}</h5><br/>
                    <h5 class="h5title">Loại sách:</h5><h5>${book.TenLoaiSach}</h5><br/>
                </div>
                <div class="mota"><p class="pmota">${book.MoTa}</p></div>
            </div>
        </div>`;
    $(`#catPr${book.MaCate_SACH}`).html(html);
}

function trangChu() {
    location.href = 'https://localhost:7282';
}
