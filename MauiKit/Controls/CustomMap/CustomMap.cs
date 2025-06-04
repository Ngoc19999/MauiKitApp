using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using MauiKit.Models;  // Giả sử MapPin đã được định nghĩa trong thư mục Models

namespace MauiKit.Controls
{
    public class CustomMap : Microsoft.Maui.Controls.Maps.Map
    {
        // BindableProperty để gán danh sách các pin tùy chỉnh từ ViewModel hoặc code-behind
        public List<CustomMapPin> CustomPins
        {
            get { return (List<CustomMapPin>)GetValue(CustomPinsProperty); }
            set { SetValue(CustomPinsProperty, value); }
        }

        // Định nghĩa BindableProperty
        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create(
                propertyName: nameof(CustomPins),
                returnType: typeof(List<CustomMapPin>),
                declaringType: typeof(CustomMap),
                defaultValue: null);
    }
}
