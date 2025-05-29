import { request, giamGiaBan } from './common.js';

let listBook = [];

function timKiem() {
    const tim = $("#txtTim").val();
    request("/Home/timKiem", 'GET', { timKiem: tim }, (response) => {
        listBook = JSON.parse(response.data);
        filltimKiem();
    });
}

function filltimKiem() {
    let html = "";
    listBook.forEach(book => {
        const giaGiam = giamGiaBan(book);
        html += `
            <div class="product" style="border: solid #F9E1E0;">
                <div class="img"><img src="${book.HinhAnh}" class="imgSach" /></div>
                <div class="ifSach">
                    <p class="pSach">${book.TenSach}</p>
                    <p class="pSach">${giaGiam} đ</p>
                    <p class="pSach"><del>${book.GiaBan} đ</del></p>
                    <input type="button" value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                    <i class="fa fa-shopping-cart"></i>
                </div>
            </div>`;
    });
    $("#center").html(html);
}

function check_BtDN() {
    const sdt = sessionStorage.getItem("sdt");
    const html = sdt
        ? `<p class="psdt">${sdt}</p>`
        : `<p class="psdt"></p>`;
    $("#sdtDN").html(html);
    $("#btdn").toggle(!sdt);
    $("#btdx").toggle(!!sdt);
}
