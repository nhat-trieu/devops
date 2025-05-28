var listGH = [];



$(document).ready(function () {
    let sdt = sessionStorage.getItem("sdt");
    if (sdt != null) {
        getListGH();
    }
})

function getListGH() {
    request(
        "/Giohangs/getGioHang",
        'GET',
        {},
        function (response) {
            if (response.success) {
                listGH = JSON.parse(response.data);
                console.log(listGH);
                fillGH();
            } else {
                alert('Hãy đăng nhập để đi đến giỏ hàng của bạn nhé!');
            }
        }
    )
}

function fillGH() {
    var html = "";
    listGH.forEach(function (sach) {
        var giaban = giamGiaBan(sach);
        console.log(giaban);
        var tongTien = giaban * sach.SoLuong;
            html += `
                    <div class="anh">
                        <img src=${sach.HinhAnh} class="imgbook"/>
                        <p class="pSachgh">${sach.TenSach}</p>
                    </div>
                    <div class="thongtin">
                        <div class ="dongia">
                                <p>${giaban} đ</p>
                        </div>
                        <div class ="soluong" style="margin-top: 15%; border: none;">
                            <input id="sl${sach.MaSach}" type="number" value=${sach.SoLuong} onchange="changeSum(${giaban})"></input>
                        </div>
                        <div id="sumSach" class ="thanhtien">
                            <p>${tongTien} đ</p>
                        </div>
                    </div>
                `

    })
    $("#ingh").html(html);
    tongTienDH();
    
}
function changeSum(giaban) {
    var soluong = $("#sl").val();
    console.log(giaban);
    console.log(soluong);
    var tongTien = giaban * soluong;
    var html = `<p>${tongTien} đ</p>`;
    $("#sumSach").html(html);
    
}
function tongTienDH() {
    var tongTien = 0;
    listGH.forEach(function (sach) {
        var giaban = giamGiaBan(sach);
        var soluong = $(`#sl${sach.MaSach}`).val();
        tongTien += giaban * soluong;
        console.log(soluong);

    })
    var html = ` <p>Tổng tiền: ${tongTien}</p>
                `;
    $("#sumDH").html(html);
}

function thanhToan() {
   
   
    var promise = [];
    listGH.forEach(function (sach) {
        var soluong = $(`#sl${sach.MaSach}`).val();
        console.log(soluong);
        var giaban = giamGiaBan(sach);
        var tongien = giaban * soluong;
        console.log(tongien);
        const data = {
            maSach: sach.MaSach,
            soLuong: soluong,
            tongTien: tongien,
            maGH: sach.MaGH
        };
        promise.push(new Promise(done => {
           
            request(
                "/Giohangs/thanhToan",
                'POST',
                data,
                function (response) {

                    done();
                 
                }
            )
        }))
        
     })
    Promise.all(promise).then(() => {
        alert('Thanh toán thành công!');
        location.href = 'https://localhost:7282/Giohangs/GioHang';
    })

}
