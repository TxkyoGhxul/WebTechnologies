using WebTechnologies.Application.Sorters.Base;
using WebTechnologies.Application.Sorters.Fields;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Sorters;

internal class UserSorter : ISorter<User, UserSortField>
{
    public IQueryable<User> Sort(IQueryable<User> models, UserSortField field, bool ascending)
    {
        return field switch
        {
            UserSortField.None => models,
            UserSortField.Name => ascending ? models.OrderBy(x => x.Name) : models.OrderByDescending(x => x.Name),
            UserSortField.BirthDate => ascending ? models.OrderBy(x => x.BirthDate) : models.OrderByDescending(x => x.BirthDate),
            //UserSortField.Age => ascending ? models.OrderBy(x => x.Age) : models.OrderByDescending(x => x.Age),
            UserSortField.Email => ascending ? models.OrderBy(x => x.Email) : models.OrderByDescending(x => x.Email),
            _ => models,
        };
    }
}
