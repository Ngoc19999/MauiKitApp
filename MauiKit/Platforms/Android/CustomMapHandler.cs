#if ANDROID
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using MauiKit.Controls;   // Namespace chứa CustomMap
using MauiKit.Models;      // Namespace chứa CustomMapPin
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Maps.Handlers;
using System;

namespace MauiKit.Platforms.Android
{
    // Custom handler kế thừa MapHandler, sử dụng IPropertyMapper mới để cập nhật CustomPins.
    public class CustomMapHandler : MapHandler
    {
        // Tạo một Property Mapper mở rộng (thừa kế từ mapper mặc định của MapHandler) 
        // để quan sát sự thay đổi của CustomPins.
        public static IPropertyMapper<CustomMap, CustomMapHandler> CustomMapper = new PropertyMapper<CustomMap, CustomMapHandler>(MapHandler.Mapper)
        {
            [nameof(CustomMap.CustomPins)] = MapCustomPins,
        };

        public CustomMapHandler() : base(CustomMapper)
        {
        }

        // Override phương thức tạo native view: Lấy instance của Android.Gms.Maps.MapView.
        protected override global::Android.Gms.Maps.MapView CreatePlatformView()
        {
            var mapView = base.CreatePlatformView();
            // Sử dụng GetMapAsync để đảm bảo rằng GoogleMap đã được tạo xong rồi gọi callback.
            mapView.GetMapAsync(new MapReadyCallback(googleMap =>
            {
                UpdatePins(googleMap);
            }));
            return mapView;
        }

        // Phương thức ánh xạ property CustomPins từ CustomMap sang handler.
        public static void MapCustomPins(CustomMapHandler handler, CustomMap map)
        {
            if (handler.PlatformView == null || map.CustomPins == null)
                return;

            // Khi có sự thay đổi trong CustomPins, cập nhật lại các marker trên bản đồ.
            handler.PlatformView.GetMapAsync(new MapReadyCallback(googleMap =>
            {
                handler.UpdatePins(googleMap);
            }));
        }

        // Phương thức cập nhật: Duyệt qua danh sách custom pins và thêm marker vào GoogleMap.
        void UpdatePins(GoogleMap googleMap)
        {
            // Xóa hết các marker cũ nếu có.
            googleMap.Clear();

            // Ép kiểu VirtualView sang CustomMap để chúng ta có thể truy cập CustomPins.
            if (!(VirtualView is CustomMap customMap) || customMap.CustomPins == null)
                return;

            // Duyệt qua tất cả custom pin được bind từ shared code.
            foreach (var pin in customMap.CustomPins)
            {
                var markerOptions = new MarkerOptions();
                // Đặt vị trí cho marker, chuyển đổi Location trong CustomMapPin thành LatLng.
                markerOptions.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));

                // Nếu có icon (chẳng hạn ghi là "darkmode") thì thực hiện tìm resource theo tên:
                if (!string.IsNullOrEmpty(pin.Icon))
                {
                    Context context = global::Android.App.Application.Context; 
                    // Tìm resource theo tên (không cần đuôi .png) trong thư mục drawable.
                    int resourceId = context.Resources.GetIdentifier(pin.Icon, "drawable", context.PackageName);
                    if (resourceId != 0)
                    {
                        // Decode resource thành Bitmap.
                        Bitmap iconBitmap = BitmapFactory.DecodeResource(context.Resources, resourceId);
                        // Gán Bitmap làm icon cho marker.
                        markerOptions.SetIcon(BitmapDescriptorFactory.FromBitmap(iconBitmap));
                    }
                }

                // Thêm marker vào GoogleMap.
                googleMap.AddMarker(markerOptions);
            }
        }
    }

    // Callback để nhận GoogleMap qua GetMapAsync.
    public class MapReadyCallback : Java.Lang.Object, IOnMapReadyCallback
    {
        private readonly Action<GoogleMap> _callback;

        public MapReadyCallback(Action<GoogleMap> callback)
        {
            _callback = callback;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _callback?.Invoke(googleMap);
        }
    }
}
#endif
