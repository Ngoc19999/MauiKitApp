using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiKit.Models
{
    public class RegisterUserDto
    {
        public string? HoTen { get; set; }
        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public DateTime? NgaySinh { get; set; }

        public byte GioiTinh { get; set; }

        public string? DiaChi { get; set; }

        public string? Avatar { get; set; }

        public string? Gplxb2 { get; set; }

        public string? Cccd { get; set; }

        public string? Lltp { get; set; }

        public string? Gksk { get; set; }

        public double Point { get; set; }

        public double Money { get; set; }

        public byte Status { get; set; }

        public byte Style { get; set; } 

        public bool IsDeleted { get; set; }
        public string? RefId { get; set; } = "0";
    }
}
