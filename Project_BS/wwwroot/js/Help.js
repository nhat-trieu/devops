function request(url, method, data, response) {
    let sdt = sessionStorage.getItem("sdt");
    let matkhau = sessionStorage.getItem("matkhau");
    $.ajax({
        url,
        type: method,
        data,
        headers: {
            'sdt': sdt,
            'matkhau': matkhau
        },
        success: function (res) {
            response(res)
        },
        error: function (error) {
            alert(error);
        }
    });
}
function giamGiaBan(book) {
    var giaBan = book.GiaBan;
    giaBan -= giaBan * 0.1;
    return giaBan;
}
function check_BtDN() {
    let sdt = sessionStorage.getItem("sdt");
    if (sdt == undefined) {
        $("#btdn").css({ display: "block" });
        $("#btdx").css({ display: "none" });
        var html = `<p class="psdt"></p>`;
        $("#sdtDN").html(html);
    } else {
        $("#btdn").css({ display: "none" });
        $("#btdx").css({ display: "block" });
        var html = `<p class="psdt">${sdt}</p>`;
        $("#sdtDN").html(html);
    }
    console.log(sdt);
}

var listBook = [];
function timKiem() {
    var tim = $("#txtTim").val();
    console.log(tim);

    request(
        "/Home/timKiem",
        'GET',
        { timKiem: tim },
        function (response) {
            if (response.success) {
                listBook = JSON.parse(response.data);
                filltimKiem();
            }
        }

    )
}
function filltimKiem() {
    var giaGiam = 0;
    var html1 = "";
    listBook.forEach(function (book) {
        giaGiam = giamGiaBan(book);
        html1 +=
            `        
                                <div class="product" style="border: solid #F9E1E0;">
                                <div class="img">
                                    <img src="${book.HinhAnh}" class="imgSach" />
                                </div>
                                <div class="ifSach">
                                    <p class="pSach">${book.TenSach}</p>
                                    <p class="pSach">${giaGiam} đ</p>
                                    <p class="pSach"><del>${book.GiaBan} đ</del></p>
                                    <input type="button" value="Mua" class="btMua" onclick="muaNgay(${book.MaSach})" >
                                    <i class="fa fa-shopping-cart" ></i>
                                </div>
                                </div>
                            `
    })
    $("#center").html(html1);
}