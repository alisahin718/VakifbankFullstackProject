﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.Messages
{
    public class OperationClaimMessages
    {
        public static string Added = "Yetki başarıyla oluşturuldu";
        public static string Updated = "Yetki başarıyla güncellendi";
        public static string Deleted = "Yetki başarıyla silindi";
        public static string NameIsNotAvaible = "Bu yetki adı daha önce kullanılmış";
    }
}
