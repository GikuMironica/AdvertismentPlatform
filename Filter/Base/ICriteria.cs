﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;
namespace AdvertismentPlatform.Filter.Base
{
    public interface ICriteria
    {
        List<Advertisment> MeetCriteria(List<Advertisment> advertisments);
    }
}
