using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCarApi1.Models.ViewModels;

namespace WebCarApi1.Models
{
    public interface ICarPersonRepository<TEntity>
    {
        IList<TEntity> List();

        TEntity Find(int id);

        IList<CarListViewModels> Find2(int id);

        void Add(TEntity entity);

        void Update(int id, TEntity entity);

        void Delete(int id);

        List<TEntity> Search(string term);

        public IList<CarListViewModels> carList();


    }
}
