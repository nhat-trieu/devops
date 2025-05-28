

var result;
var check;

function DangKy() {
    var sdt = $("#sdt").val();
    var matkhau = $("#pass").val();
    console.log(sdt);
    console.log(matkhau);
    const data = {
        sdt: sdt,
        matkhau: matkhau
    };
    request(
        "/DangNhap/DangKy",
        'POST',
        data,
        function (response) {
            if (response.success) {
                result = JSON.parse(response.data);
                console.log(result);
                if (result != 0) {
                    alert('Đăng ký thành công!');
                    sessionStorage.setItem("sdt", sdt);
                    sessionStorage.setItem("matkhau", matkhau);
                    location.href = 'https://localhost:7282';
                } else {
                    alert('Tài khoản này đã tồn tại. Vui lòng kiểm tra lại!');
                }
            }
        }

    )
}

function DangNhap() {
    var sdt = $("#sdt").val();
    var matkhau = $("#pass").val();
    console.log(sdt);
    console.log(matkhau);
    const data = {
        sdt: sdt,
        matkhau: matkhau
    };
    request(
        "/DangNhap/DangNhap",
        'POST',
        data,
        function (response) {
            if (response.success) {
                check = JSON.parse(response.data);
                console.log(check);
                if (check != 0) {
                    alert('Đăng nhập thành công!');
                    sessionStorage.setItem("sdt", sdt);
                    sessionStorage.setItem("matkhau", matkhau);
                    location.href = 'https://localhost:7282';
                } else {
                    alert('Tài khoản hoặc mật khẩu không đúng. Vui lòng kiểm tra lại');
                }
            }
        }

    )
}




