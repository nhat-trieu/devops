var listCatLevel0 = [];
var listProduct = [];
/*
{
    2: [cat childs cua 2],
    1: [cat childs cua 1]
};
*/
var dictionaryCatLevel1 = {};
var dictionaryCatLevel2 = {};
var dictionaryCat_SanPham = {};


$(document).ready(function() {
    getListCatLevel0();

    let sdt = sessionStorage.getItem("sdt");
    let matkhau = sessionStorage.getItem("matkhau");
    check_BtDN();
})

// Data
function getListCatLevel0() {
    // GET data from controller/server
    request(
        "/Home/ListCat",
        'GET',
        { parentId: null },
        function (response) {
            if (response.success) {
                listCatLevel0 = JSON.parse(response.data);
                var html = "";
                for (let i = 0; i < listCatLevel0.length; i++) {
                    const cat = listCatLevel0[i];
                    html += 
                    `
                    <li class="menu0_item" onmouseover="onCatHoverIn(${cat.MaCate})">${cat.TenDanhMuc}</li>
                    `
                }
                $("#menu_level0").html(html);

                getListCatLevel1();
            }
        }
    )
}

function getListCatLevel1() {
    var promise = [];
    
    listCatLevel0.forEach(function (category) {
        // GET data from controller/server
        promise.push(new Promise(done => {
            request(
                "/Home/ListCat",
                'GET',
                { parentId: category.MaCate },
                function (response) {
                    if (response.success) {
                        const listCatLevel1 = JSON.parse(response.data);
                        dictionaryCatLevel1[category.MaCate] = listCatLevel1;

                    }
                    done();
                }
            )
        }))
    })
    Promise.all(promise).then(() => {
        fillMenuChildForFirstCat0();
        getListCatLevel2();
        
    })
}

function getListCatLevel2() {
    var promise = [];
    listCatLevel0.forEach(function (catLevel0) {
        const listCatLevel1 = dictionaryCatLevel1[catLevel0.MaCate];
        listCatLevel1.forEach(function (category) {
            promise.push(new Promise(done => {

                request(
                    "/Home/ListCat",
                    'GET',
                    { parentId: category.MaCate },
                    function (response) {
                        if (response) {
                            const listCatLevel2 = JSON.parse(response.data);
                            dictionaryCatLevel2[category.MaCate] = listCatLevel2;

                        }
                        done();
                    }
                   
                )
            }))
        })
    })

    Promise.all(promise).then(() => {
        getListProduct();
        fillMenuChildForCat1();
       
    })
}

function fillMenuChildForFirstCat0() {
    const cat0 = listCatLevel0[0];

    const cat0Childs = dictionaryCatLevel1[cat0.MaCate];
    if (cat0Childs !== undefined) {
        var html = "";
        cat0Childs.forEach(function (catLevel1) {
            html +=
            `
            <div class="menuchild_item">
                <h4>${catLevel1.TenDanhMuc}</h4>
                <ul id = "menucua${catLevel1.MaCate}" class = "ul_munuchild"></ul>
            </div>
            `
        })
        $("#menuchild").html(html);
    }
    
}
function fillMenuChildForCat1() {

   
    const listCateLevel1 = dictionaryCatLevel1[listCatLevel0[0].MaCate];
    listCateLevel1.forEach(function (cat1) {
        const listCateLevel2 = dictionaryCatLevel2[cat1.MaCate]
        var html = "";
        listCateLevel2.forEach(function (catLevel2) {
            if (dictionaryCatLevel2[cat1.MaCate] != undefined) {
               
                html +=
                    `<a onclick="product_Category(${catLevel2.MaCate}, '${catLevel2.TenDanhMuc}')">${catLevel2.TenDanhMuc}</a> <br/>`
            } 
        })
        $(`#menucua${cat1.MaCate}`).html(html);
    })
}


// Actions
function onMenuHoverIn() {
    const row = document.getElementById("row");
    row.style.display = 'block';
    fillMenuChildForFirstCat0();
    fillMenuChildForCat1();
}

function onMenuHoverOut() {
    const row = document.getElementById("row");
    row.style.display = 'none';
}
function onCatHoverIn(catId) {

    const listCatLevel1 = dictionaryCatLevel1[catId];
    
    var html = "";
    listCatLevel1.forEach(function (catLevel1) {
        html +=
        `
        <div class="menuchild_item">
            <h4>${catLevel1.TenDanhMuc}</h4>
             <ul id = "menucua${catLevel1.MaCate}" class = "ul_munuchild"></ul>
        </div>
        `
    })
    $("#menuchild").html(html);
    listCatLevel1.forEach(function (catLevel1) {
        const listCatLevel2 = dictionaryCatLevel2[catLevel1.MaCate];
        var html = "";
        listCatLevel2.forEach(function (catLevel2) {
            html += 
                `<a onclick="product_Category(${catLevel2.MaCate}, '${catLevel2.TenDanhMuc}')">${catLevel2.TenDanhMuc}</a> <br/>`
        })
        $(`#menucua${catLevel1.MaCate}`).html(html);

    })
    


    
}


function getListProduct() {
    var promise = [];
    listCatLevel0.forEach(function (catLv0) {
        const listCatLevel1 = dictionaryCatLevel1[catLv0.MaCate];
        listCatLevel1.forEach(function (catLv1) {
            const listCatLevel2 = dictionaryCatLevel2[catLv1.MaCate];
            listCatLevel2.forEach(function (catLv2) {
                promise.push(new Promise(done => {
                    request(
                        "/Home/ListProduct",
                        'GET',
                        { maCatSach: catLv2.MaCate },
                        function (response) {
                            if (response.success) {
                                listProduct = JSON.parse(response.data);
                                dictionaryCat_SanPham[catLv2.MaCate] = listProduct;
                            }
                            done()
                        }
                       
                    )
               }))
                
            })
        })
    })
    Promise.all(promise).then(() => {
        fillProduct();
    })
} 

function fillProduct() {
    
    var html = "";
    listCatLevel0.forEach(function (catLv0) {
        const listCatLevel1 = dictionaryCatLevel1[catLv0.MaCate];
        listCatLevel1.forEach(function (catLv1) {
            const listCatLevel2 = dictionaryCatLevel2[catLv1.MaCate];
            listCatLevel2.forEach(function (catLv2) {
                const listProduct = dictionaryCat_SanPham[catLv2.MaCate];
               
                if (listProduct.length != 0) {
                    html += `
                             <div id="catPr${catLv2.MaCate}" class="catProduct">
                             </div>
                             `
                }
               
            })
        })
    })
    $("#listPr").html(html);
    listCatLevel0.forEach(function (catLv0) {
        const listCatLevel1 = dictionaryCatLevel1[catLv0.MaCate];
        listCatLevel1.forEach(function (catLv1) {
            const listCatLevel2 = dictionaryCatLevel2[catLv1.MaCate];
            listCatLevel2.forEach(function (catLv2) {
                const listProduct = dictionaryCat_SanPham[catLv2.MaCate];
                if (listProduct.length != 0) {
                    var giaGiam = 0;
                    var html1 = "";
                    html1 += `<label>${catLv2.TenDanhMuc}</label> <br/>`;
                    listProduct.forEach(function (book) {
                        giaGiam = giamGiaBan(book);
                        html1 +=
                            `        
                                <div   class="product">
                                <div class="img" onclick="chiTietSach(${book.MaSach})">
                                    <img src="${book.HinhAnh}" class="imgSach" />
                                </div>
                                <div class="ifSach">
                                    <p class="pSach">${book.TenSach}</p>
                                    <p class="pSach">${giaGiam} đ</p>
                                    <p class="pSach" style="color:red;"><del>${book.GiaBan} đ</del></p>
                                    <input type="button" style="background-color: #FED361;" " value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                                    <i class="fa fa-shopping-cart" onclick="themGH(${book.MaSach})"></i>
                                </div>
                                </div>
                            `
                    })
                    $(`#catPr${catLv2.MaCate}`).html(html1);
                }

            })
        })

    })
}

function product_Category(catID, tenDM) {
    const listProduct = dictionaryCat_SanPham[catID];
    var html = `
                    <div id="onlycatPr${catID}" class="catProduct">
                    </div>
               `
    $("#listPr").html(html);
    var giaGiam = 0;
    var html1 = "";
    html1 += `<label>${tenDM}</label> <br/>`;
    listProduct.forEach(function (book) {
        giaGiam = giamGiaBan(book);
        html1 +=
            `        
                                <div class="product">
                                <div class="img" onclick="chiTietSach(${book.MaSach})">
                                    <img src="${book.HinhAnh}" class="imgSach" />
                                </div>
                                <div class="ifSach">
                                    <p class="pSach">${book.TenSach}</p>
                                    <p class="pSach">${giaGiam} đ</p>
                                    <p class="pSach" style="color:red;"><del>${book.GiaBan} đ</del></p>
                                    <input type="button" style="background-color: #FED361;" " value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                                    <i class="fa fa-shopping-cart" onclick="themGH(${book.MaSach})" ></i>
                                </div>
                                </div>
                            `
    })
    $(`#onlycatPr${catID}`).html(html1);
}



function muaNgay(maSach) {
   
    request(
        "/Home/muaNgay",
        'POST',
        {maSach: maSach},
        function (response) {
            if (response.success) {
                location.href = 'https://localhost:7282/Giohangs/GioHang';
            } else {
                alert('Mua khong thanh cong!');
            }
        }

    )
}

function trangChu(){
    location.href = 'https://localhost:7282';
}
function dangXuat() {
    sessionStorage.removeItem("sdt");
    sessionStorage.removeItem("matkhau");
    check_BtDN();
    location.href = 'https://localhost:7282';
}
function themGH(maSach) {
    request(
        "/Home/muaNgay",
        'POST',
        { maSach: maSach },
        function (response) {
            if (response.success) {
                alert('Đã thêm vào giỏ hàng!');
            } else {
                alert('Hãy đăng nhập để thêm sản phẩm vào giỏ hàng của bạn nhé!');
            }
        }

    )
}


var chitietsach = [];
function chiTietSach(maSach) {
    request(
        "/Home/chiTietSach",
        'GET',
        { maSach: maSach },
        function (response) {
            if (response.success) {
                chitietsach = JSON.parse(response.data);
                fillChiTiet();
            } 
        }

    )
}
function fillChiTiet(){
    var html =  `
                    <div id="catPr${chitietsach[0].MaCate_SACH}" class="catProduct">
                    </div>
                    `
    $("#listPr").html(html);
    var giaGiam = giamGiaBan(chitietsach[0]);
    var html1 = `
                    <div class="productchitiet" >
                                <div class="img" onclick="chiTietSach(${chitietsach[0].MaSach})">
                                    <img src="${chitietsach[0].HinhAnh}" class="imgSach" />
                                </div>
                                <div class="ifSach">
                                    <p class="pSach">${chitietsach[0].TenSach}</p>
                                    <p class="pSach">${giaGiam} đ</p>
                                    <p class="pSach" style="color:red;"><del>${chitietsach[0].GiaBan} đ</del></p>
                                    <input type="button" style="background-color: #FED361;" " value="Mua" class="btMua" onclick="muaNgay(${chitietsach[0].MaSach})" >
                                    <i class="fa fa-shopping-cart" onclick="themGH(${chitietsach[0].MaSach})"></i>
                                </div>
                                </div>
                    <div class ="productchitiet">
                        <div>
                        <p class="pTenchitiet" >${chitietsach[0].TenSach}</p>
                        </div>
                        <div class="chitiet">
                            <div class="motachitiet">
                                <h5 class="h5title" >Tác giả: </h5>
                                <h5>${chitietsach[0].TenTacGia}</h5> <br/>
                                <h5 class="h5title" >NXB: </h5>
                                <h5>${chitietsach[0].TenNXB}</h5> <br/>
                                <h5 class="h5title" >NCC: </h5>
                                <h5>${chitietsach[0].TenNCC}</h5> <br/>
                                <h5 class="h5title">Loại sách: </h5>
                                <h5>${chitietsach[0].TenLoaiSach}</h5> <br/>
                            </div>
                            <div class="mota">
                                 <p class="pmota">${chitietsach[0].MoTa}</p>
                            </div>
                       <div/>
                    </div>
                `

    $(`#catPr${chitietsach[0].MaCate_SACH}`).html(html1);      
}