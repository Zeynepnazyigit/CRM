using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    // Identity teknolojisi kullanılacaksa kullanıcı yapısı da bu kütüphaneden sağşanır , ek özellikler içinse kalıtım alınır.

    // IdentityUser<string> : Kullanmak istenilen primary key veri tipi jenerik olarak yazılır, güvenlik için 'string' tercih edilmiştir.
    public class ApplicationUser: IdentityUser
    {
        //  IdentityUser sınıfından gelen özelliklere ek olarak kullanıcıya ait ekstra bilgiler burada tutulabilir.
        // Id (string) , UserName, Email, EmailConfirmed, PhoneNumber, PhoneNumberConfirmed, PasswordHash , SecurityStamp , LockoutEnd , LockoutEnabled , AccessFailedCount gibi özellikler IdentityUser sınıfında tanımlıdır.

        public string FirstName { get; set; } = null!; // null! : Bu alanın (string) boş olmayacağını garanti ettiğimizi belirtir.
          public string LastName { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; } // ?: Bu alanın boş olabileceğini belirtir.
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Activity>? Activities { get; set; } = [];
        public virtual ICollection<Opportunity>? Opportunities { get; set; } = []; 
        public virtual ICollection<Customer> Customers { get; set; } = [];
        public virtual ICollection<Lead> Leads { get; set; } = [];
    }
}
