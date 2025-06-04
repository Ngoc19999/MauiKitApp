using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;

namespace MauiKit.Models  // Hãy thay YourProjectNamespace bằng namespace thực của dự án bạn
{
    public class CustomMapPin
    {
        // Mã định danh duy nhất cho pin
        public string Id { get; set; }

        // Vị trí của pin trên bản đồ
        public Location Position { get; set; }

        // Tên hoặc đường dẫn biểu tượng cho pin (để sử dụng hình ảnh tùy chỉnh)
        public string Icon { get; set; }

        // Lệnh thực hiện khi người dùng nhấn vào pin
        public ICommand ClickedCommand { get; set; }

        // Hàm khởi tạo, nhận vào một delegate để xử lý sự kiện click
        public CustomMapPin(Action<CustomMapPin> clicked)
        {
            ClickedCommand = new Command(() => clicked(this));
        }
    }
}
