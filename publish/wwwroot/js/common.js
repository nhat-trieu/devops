// common.js

export function request(url, method, data, callback) {
    const sdt = sessionStorage.getItem("sdt");
    const matkhau = sessionStorage.getItem("matkhau");

    $.ajax({
        url,
        type: method,
        data,
        headers: {
            'sdt': sdt,
            'matkhau': matkhau
        },
        success: callback,
        error: function (error) {
            alert(error);
        }
    });
}

export function giamGiaBan(book) {
    return book.GiaBan - (book.GiaBan * 0.1);
}

export function isLoggedIn() {
    return sessionStorage.getItem("sdt") !== null;
}

export function dangXuat() {
    sessionStorage.removeItem("sdt");
    sessionStorage.removeItem("matkhau");
    location.href = 'https://localhost:7282';
}

export function muaNgay(maSach) {
    request("/Home/muaNgay", 'POST', { maSach }, (response) => {
        if (response.success) {
            location.href = 'https://localhost:7282/Giohangs/GioHang';
        } else {
            alert('Mua không thành công!');
        }
    });
}

export function themGH(maSach) {
    request("/Home/muaNgay", 'POST', { maSach }, (response) => {
        if (response.success) {
            alert('Đã thêm vào giỏ hàng!');
        } else {
            alert('Hãy đăng nhập để thêm sản phẩm vào giỏ hàng của bạn nhé!');
        }
    });
}
