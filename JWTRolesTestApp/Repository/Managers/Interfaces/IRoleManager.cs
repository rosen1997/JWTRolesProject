﻿using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository.Managers.Interfaces
{
    public interface IRoleManager : IRepositoryBase<Role>
    {
    }
}
