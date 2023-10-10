namespace WebTechnologies.Application.Sorters.Base;

internal interface ISorter<TModel, TField>
{
    IQueryable<TModel> Sort(IQueryable<TModel> models, TField field, bool ascending);
}