using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        // burada bir join atacağız exstradan operasyon koyduk
        List<OperationClaim> GetClaims(User user);
    }
}
