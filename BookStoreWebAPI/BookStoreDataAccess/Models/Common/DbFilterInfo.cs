﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDataAccess.Models.Common
{
    public class DbFilterInfo
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public string SearchValue { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }
    }
}
