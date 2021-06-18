--DROP DATABASE QLKS

--USE master
--CREATE DATABASE QLKS

use QLKS
create table HoaDon(
	ma_hd int NOT NULL IDENTITY(1,1) primary key,
	ma_nv int,
	ma_pdp int,
	ngay_tra_phong datetime,
	ma_tinh_trang int,
	tien_phong float
);
create table ChucVu(
	ma_chuc_vu int NOT NULL IDENTITY(1,1) primary key,
	chuc_vu nvarchar(50)
);
create table KhachHang(
	ma_kh nvarchar(50) primary key,
	mat_khau nvarchar(50),
	ho_ten nvarchar(50),
	cmt nvarchar(30),
	sdt nvarchar(15),
	mail nvarchar(100),
	diem int
);
create table LoaiPhong(
	loai_phong int NOT NULL IDENTITY(1,1) primary key,
	mo_ta nvarchar(50),
	gia float,
	ti_le_phu_thu int,
	anh nvarchar(300)
);
create table Tang(
	ma_tang int IDENTITY(1,1) primary key,
	ten_tang nvarchar(20)
);
create table NhanVien(
	ma_nv int IDENTITY(1,1) primary key,
	ho_ten nvarchar(50),
	ngay_sinh date,
	dia_chi nvarchar(100),
	sdt nvarchar(15),
	tai_khoan nvarchar(50),
	mat_khau nvarchar(50),
	ma_chuc_vu int
);
create table Phong(
	ma_phong int IDENTITY(1,1) primary key,
	so_phong nvarchar(10),
	loai_phong int,
	ma_tang int,
	ma_tinh_trang int
);
create table TinhTrangPhong(
	ma_tinh_trang int IDENTITY(1,1) primary key,
	mo_ta nvarchar(50)
);
create table TinhTrangHoaDon(
	ma_tinh_trang int IDENTITY(1,1) primary key,
	mo_ta nvarchar(50)
);
create table PhieuDatPhong(
	ma_pdp int IDENTITY(1,1) primary key,
	ma_kh nvarchar(50),
	ngay_dat datetime,
	ngay_vao datetime,
	ngay_ra datetime,
	ma_phong int,
	ma_tinh_trang int
);
create table TinhTrangPhieuDatPhong(
	ma_tinh_trang int IDENTITY(1,1) primary key,
	tinh_trang nvarchar(50)
);
create table TinNhan(
	id int IDENTITY(1,1) primary key,
	ngay_gui datetime,
	ma_kh nvarchar(50),
	ho_ten nvarchar(100),
	mail nvarchar(100),
	noi_dung nvarchar(500)
);

alter table TinNhan
add CONSTRAINT fk_tin_nhan
FOREIGN KEY (ma_kh) REFERENCES KhachHang(ma_kh);

alter table NhanVien
add CONSTRAINT fk_ma_cv
FOREIGN KEY (ma_chuc_vu) REFERENCES ChucVu(ma_chuc_vu);

alter table HoaDon
add CONSTRAINT fk_ma_nv
FOREIGN KEY (ma_nv) REFERENCES NhanVien(ma_nv);

--alter table HoaDon
--add CONSTRAINT fk_ma_kh
--FOREIGN KEY (ma_kh) REFERENCES tblKhachHang(ma_kh);

alter table Phong
add CONSTRAINT fk_ma_lp
FOREIGN KEY (loai_phong) REFERENCES loaiPhong(loai_phong);

alter table Phong
add CONSTRAINT fk_ma_tang
FOREIGN KEY (ma_tang) REFERENCES Tang(ma_tang);

--alter table HoaDon
--add CONSTRAINT fk_ma_phong
--FOREIGN KEY (ma_phong) REFERENCES tblPhong(ma_phong);

alter table HoaDon
add CONSTRAINT fk_ma_pdp
FOREIGN KEY (ma_pdp) REFERENCES PhieuDatPhong(ma_pdp);

alter table HoaDon
add CONSTRAINT fk_ma_tthd
FOREIGN KEY (ma_tinh_trang) REFERENCES TinhTrangHoaDon(ma_tinh_trang);

alter table Phong
add CONSTRAINT fk_ma_tt_2
FOREIGN KEY (ma_tinh_trang) REFERENCES TinhTrangPhong(ma_tinh_trang);

alter table PhieuDatPhong
add CONSTRAINT fk_tgd_ma_kh2
FOREIGN KEY (ma_kh) REFERENCES KhachHang(ma_kh);

alter table PhieuDatPhong
add CONSTRAINT fk_tgd_ma_phong_2
FOREIGN KEY (ma_phong) REFERENCES Phong(ma_phong);

alter table PhieuDatPhong
add CONSTRAINT fk_tgd_tt_2
FOREIGN KEY (ma_tinh_trang) REFERENCES TinhTrangPhieuDatPhong(ma_tinh_trang);

insert into ChucVu values(N'Quản trị');
insert into ChucVu values(N'Quản lý');
insert into ChucVu values(N'Nhân viên');

insert into NhanVien values(N'My Anh','','',N'01677915896',N'admin',N'12345',1);
insert into NhanVien values(N'Anh My','','',N'01677915896',N'min',N'12345',2);
insert into NhanVien values(N'Nguyen My Anh','','',N'01677915896',N'max',N'12345',3);

insert into LoaiPhong values(N'Phòng đơn','100000',20,'["/Content/Images/Phong/11.jpg","/Content/Images/Phong/12.jpg","/Content/Images/Phong/13.jpg"]');
insert into LoaiPhong values(N'Phòng đôi','150000',25,'["/Content/Images/Phong/21.jpg","/Content/Images/Phong/22.jpg","/Content/Images/Phong/23.jpg"]');
insert into LoaiPhong values(N'Phòng vip','200000',30,'["/Content/Images/Phong/31.jpg","/Content/Images/Phong/32.jpg"]');

insert into Tang values(N'Tầng 1');
insert into Tang values(N'Tầng 2');
insert into Tang values(N'Tầng 3');

insert into TinhTrangPhong values(N'Trống');
insert into TinhTrangPhong values(N'Đang sử dụng');
insert into TinhTrangPhong values(N'Đang dọn');
insert into TinhTrangPhong values(N'Đang bảo trì');
insert into TinhTrangPhong values(N'Dừng sử dụng');

insert into TinhTrangHoaDon values(N'Chưa thanh toán');
insert into TinhTrangHoaDon values(N'Đã thanh toán');

insert into Phong values('101','1','1','1');
insert into Phong values('102','1','1','1');
insert into Phong values('103','1','1','1');
insert into Phong values('104','2','1','1');
insert into Phong values('105','2','1','1');
insert into Phong values('106','3','1','1');

insert into Phong values('201','1','2','1');
insert into Phong values('202','1','2','1');
insert into Phong values('203','1','2','1');
insert into Phong values('204','2','2','1');
insert into Phong values('205','2','2','1');
insert into Phong values('206','3','2','1');

insert into Phong values('301','1','3','1');
insert into Phong values('302','1','3','1');
insert into Phong values('303','1','3','1');
insert into Phong values('304','2','3','1');
insert into Phong values('305','2','3','1');
insert into Phong values('306','3','3','1');

insert into KhachHang values(N'myanh',N'12345',N'My Anh ',N'123456789',N'0303030303','myanh@gmail.com',0);
insert into KhachHang values(N'myanh2',N'12345',N'My Anh 2',N'123456789',N'0303030303','myanh2@gmail.com',0);
insert into KhachHang values(N'myanh3',N'12345',N'My Anh 3',N'123456789',N'0303030303','myanh3@gmail.com',0);

insert into TinhTrangPhieuDatPhong values(N'Đang đặt');
insert into TinhTrangPhieuDatPhong values(N'Đã xong');
insert into TinhTrangPhieuDatPhong values(N'Đã hủy');
insert into TinhTrangPhieuDatPhong values(N'Đã thanh toán');

insert into PhieuDatPhong values(N'myanh','2021-04-19 21:35:28.170','2021-04-19 21:35:28.170','2021-04-26 21:35:28.170','7',4);
insert into PhieuDatPhong values(N'myanh2','2021-04-18 21:35:28.170','2021-04-18 21:35:28.170','2021-04-28 21:35:28.170','8',4);
insert into PhieuDatPhong values(N'myanh3','2021-04-21 21:35:28.170','2021-04-21 21:35:28.170','2021-04-29 21:35:28.170','9',4);
insert into PhieuDatPhong values(N'myanh','2021-04-19 21:35:28.170','2021-04-19 21:35:28.170','2021-04-21 21:35:28.170','7',4);
insert into PhieuDatPhong values(N'myanh2','2021-04-18 21:35:28.170','2021-04-18 21:35:28.170','2021-04-22 21:35:28.170','8',4);
insert into PhieuDatPhong values(N'myanh3','2021-04-21 21:35:28.170','2021-04-21 21:35:28.170','2021-04-23 21:35:28.170','9',4);
insert into PhieuDatPhong values(N'myanh','2021-04-22 21:35:28.170','2021-04-22 21:35:28.170','2021-04-24 21:35:28.170','7',4);
insert into PhieuDatPhong values(N'myanh2','2021-04-22 21:35:28.170','2021-04-22 21:35:28.170','2021-04-25 21:35:28.170','8',4);
insert into PhieuDatPhong values(N'myanh3','2021-04-24 21:35:28.170','2021-04-26 21:35:28.170','2021-04-27 21:35:28.170','9',4);
insert into PhieuDatPhong values(N'myanh','2021-04-25 21:35:28.170','2021-04-27 21:35:28.170','2021-04-30 21:35:28.170','1',4);
insert into PhieuDatPhong values(N'myanh2','2021-04-27 21:35:28.170','2021-04-27 21:35:28.170','2021-05-01 21:35:28.170','2',4);
insert into PhieuDatPhong values(N'myanh3','2021-04-30 21:35:28.170','2021-05-02 21:35:28.170','2021-05-03 21:35:28.170','3',4);
insert into PhieuDatPhong values(N'myanh','2021-05-01 21:35:28.170','2021-05-01 21:35:28.170','2021-05-04 21:35:28.170','4',4);
insert into PhieuDatPhong values(N'myanh2','2021-05-03 21:35:28.170','2021-05-03 21:35:28.170','2021-05-05 21:35:28.170','5',4);
insert into PhieuDatPhong values(N'myanh3','2021-05-04 21:35:28.170','2021-05-04 21:35:28.170','2021-05-05 21:35:28.170','6',4);

insert into HoaDon values('1','1','2021-04-24 21:36:27.427','2','800000');
insert into HoaDon values('1','2','2021-04-25 21:36:27.427','2','1100000');
insert into HoaDon values('1','3','2021-04-27 21:36:27.427','2','900000');
insert into HoaDon values('1','4','2021-04-21 21:36:27.427','2','300000');
insert into HoaDon values('1','5','2021-04-22 21:36:27.427','2','500000');
insert into HoaDon values('1','6','2021-04-23 21:36:27.427','2','200000');
insert into HoaDon values('1','7','2021-04-24 21:36:27.427','2','300000');
insert into HoaDon values('1','8','2021-04-25 21:36:27.427','2','400000');
insert into HoaDon values('1','9','2021-04-27 21:36:27.427','2','200000');
insert into HoaDon values('1','10','2021-04-30 21:36:27.427','2','400000');
insert into HoaDon values('1','11','2021-05-01 21:36:27.427','2','500000');
insert into HoaDon values('1','12','2021-05-03 21:36:27.427','2','200000');
insert into HoaDon values('1','13','2021-05-04 21:36:27.427','2','600000');
insert into HoaDon values('1','14','2021-05-05 21:36:27.427','2','450000');
insert into HoaDon values('1','15','2021-05-05 21:36:27.427','2','400000');