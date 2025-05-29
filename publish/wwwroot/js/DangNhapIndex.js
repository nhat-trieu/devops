import { request } from './common.js';

function DangKy() {
    const sdt = $("#sdt").val();
    const matkhau = $("#pass").val();

    const data = { sdt, matkhau };
    request("/DangNhap/DangKy", 'POST', data, (response) => {
        const result = JSON.parse(response.data);
        if (result !== 0) {
            alert('Đăng ký thành công!');
            sessionStorage.setItem("sdt", sdt);
            sessionStorage.setItem("matkhau", matkhau);
            location.href = 'https://localhost:7282';
        } else {
            alert('Tài khoản đã tồn tại. Vui lòng kiểm tra lại!');
        }
    });
}

function DangNhap() {
    const sdt = $("#sdt").val();
    const matkhau = $("#pass").val();

    const data = { sdt, matkhau };
    request("/DangNhap/DangNhap", 'POST', data, (response) => {
        const check = JSON.parse(response.data);
        if (check !== 0) {
            alert('Đăng nhập thành công!');
            sessionStorage.setItem("sdt", sdt);
            sessionStorage.setItem("matkhau", matkhau);
            location.href = 'https://localhost:7282';
        } else {
            alert('Sai tài khoản hoặc mật khẩu!');
        }
    });
}
