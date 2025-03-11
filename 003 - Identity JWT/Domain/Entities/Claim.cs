using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Interfaces.ORM;

namespace Domain.Entities
{
    public class Claim:BaseEntity,IEntity
    {
        public string Name { get; set; } // Yetkilendirme Adı (örn: "CanEditUsers")
        public string Description { get; set; } // Yetkinin ne işe yaradığını açıklayan metin (Opsiyonel)

        public ICollection<UserClaim> UserClaims { get; set; } // Bu yetkiye sahip kullanıcıları tutar
        public ICollection<RoleClaim> RoleClaims { get; set; } // Bu yetkiye sahip rolleri tutar
    }
}