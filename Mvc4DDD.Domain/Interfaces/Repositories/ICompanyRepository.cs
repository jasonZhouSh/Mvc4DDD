﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using Mvc4DDD.Domain.Entities;

namespace Mvc4DDD.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        IEnumerable<String> GetCountries();

        Dictionary<String, String> GetCategories();

        IEnumerable<Company> GetByLocation(String location);

        IEnumerable<Company> GetByCategory(String categoryCode);
    }
}
