## Bài 1: Liệt kê thư mục và file (Tree App)


Trong Linux có một ứng dụng nổi tiếng có tên là tree để liệt kê toàn bộ thư mục và thư mục con + file
```
$ tree .
.
├── ReadMe.md
└── folderA
    ├── demo.cs
    ├── folderAA
    │   └── file.cs
    └── util.cs 
```
- liệt kê thư mục hiện thời
- liệt kê thư mục cha
- liệt kê thư mục người dùng.

### Ý tưởng thực hiện bài toán:
- Directory.GetDirectories(path) : lấy ra Subfolder, Directory.GetFiles(d): lấy ra file.
- Dùng loop thứ nhất để lấy ra Subfolder đầu tiên và lồng loop thứ 2 để lấy ra file trong folder đó
- Dùng **đệ quy** để  lấy hết được SubFolder và File tiếp theo.

### Để run app:
Sau khi **git clone** , cd đến thư mục TreeApp
- Dùng lệnh **dotnet run** để run app, app sẽ lấy ra hết các subfolder và file trong thư mục hiện có.
- Có thể bỏ phần comment trong hàm main để lấy ra subfolder và file trong thư mục muốn nhập.

