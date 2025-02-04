Use TrungNT65
GO
-- Xuất ra Danh sách tên bộ môn và số lượng giáo viên của bộ môn đó KHI số lượng giáo viên lớn hơn 2
SELECT TENBM, COUNT(*) AS 'Số Giáo Viên' FROM BOMON AS BM, GIAOVIEN AS GV
WHERE BM.MABM = GV.MABM
GROUP BY BM.TENBM HAVING COUNT(*) >= 2
GO
-- Lấy ra danh sách giáo viên có lương > lương trung bình của Giáo viên
SELECT * FROM GIAOVIEN 
WHERE LUONG > ((SELECT SUM(LUONG) FROM GIAOVIEN)/(SELECT COUNT(*) FROM GIAOVIEN))
GO
-- Xuất ra tên giáo viên và số lượng đề tài giáo viên đó đã làm
SELECT GV.HOTEN, COUNT(*) AS 'Số DT' FROM THAMGIADT AS DT , GIAOVIEN AS GV 
WHERE GV.MAGV = DT.MAGV
GROUP BY GV.HOTEN
GO
/*
Bài tập:
1. Xuất ra tên giáo viên và số lượng người thân của GV đó
2. Xuât ra tên giáo viên và số lượng đề tài đã hoàn thành mà giáo viên đó tham gia
*/
--1. Xuất ra tên giáo viên và số lượng người thân của GV đó
SELECT GV.HOTEN, COUNT(*) AS 'Số NT' FROM NGUOITHAN AS NT , GIAOVIEN AS GV 
WHERE GV.MAGV = NT.MAGV
GROUP BY GV.HOTEN
--2. Xuât ra tên giáo viên và số lượng đề tài đã hoàn thành mà giáo viên đó tham gia
SELECT GV.HOTEN, COUNT(*) AS 'DTHT' FROM THAMGIADT AS DT , GIAOVIEN AS GV 
WHERE GV.MAGV = DT.MAGV AND DT.KETQUA LIKE N'%t'
GROUP BY GV.HOTEN
GO
