﻿using Core.DataAccess;
using DataAccess.Context;
using DataAccess.Repositories.Contract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFCore
{
    public class UserOperationClaimRepository : RepositoryBase<UserOperationClaim>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(RepositoryContext context) : base(context)
        {

        }
    }
}
