import { request, giamGiaBan } from './common.js';

let listGH = [];

$(document).ready(function () {
    const sdt = sessionStorage.getItem("sdt");
    if (sdt) getListGH();
});

function getListGH() {
    request("/Giohangs/getGioHang", 'GET', {}, (response) => {
        listGH = JSON.parse(response.data);
        fillGH();
    });
}

function fillGH() {
    let html = "";
    listGH.forEach(sach => {
        const giaban = giamGiaBan(sach);
        const tongTien = giaban * sach.SoLuong;
        html += `
            <div class="anh"><img src=${sach.HinhAnh} class="imgbook"/><p class="pSachgh">${sach.TenSach}</p></div>
            <div class="thongtin">
                <div class="dongia"><p>${giaban} đ</p></div>
                <div class="soluong" style="margin-top: 15%; border: none;">
                    <input id="sl${sach.MaSach}" type="number" value=${sach.SoLuong} onchange="changeSum(${giaban})" />
                </div>
                <div id="sumSach" class="thanhtien"><p>${tongTien} đ</p></div>
            </div>`;
    });
    $("#ingh").html(html);
    tongTienDH();
}

function changeSum(giaban) {
    const soluong = $("#sl").val();
    const tongTien = giaban * soluong;
    $("#sumSach").html(`<p>${tongTien} đ</p>`);
}

function tongTienDH() {
    let tongTien = 0;
    listGH.forEach(sach => {
        const giaban = giamGiaBan(sach);
        const soluong = $(`#sl${sach.MaSach}`).val();
        tongTien += giaban * soluong;
    });
    $("#sumDH").html(`<p>Tổng tiền: ${tongTien}</p>`);
}

function thanhToan() {
    const promises = listGH.map(sach => {
        const soluong = $(`#sl${sach.MaSach}`).val();
        const giaban = giamGiaBan(sach);
        const tongTien = giaban * soluong;
        const data = {
            maSach: sach.MaSach,
            soLuong: soluong,
            tongTien: tongTien,
            maGH: sach.MaGH
        };
        return new Promise(done => {
            request("/Giohangs/thanhToan", 'POST', data, () => done());
        });
    });

    Promise.all(promises).then(() => {
        alert('Thanh toán thành công!');
        location.href = 'https://localhost:7282/Giohangs/GioHang';
    });
}
