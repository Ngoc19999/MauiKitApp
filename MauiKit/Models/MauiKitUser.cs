using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiKit.Models
{
    public class MauiKitUser
    {
        public string HoTen { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public byte Style { get; set; }
        public string? DiaChi { get; set; }
        public byte GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }

    }
}
