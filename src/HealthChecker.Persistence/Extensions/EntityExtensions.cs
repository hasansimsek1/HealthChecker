using System;

namespace HealthChecker.Persistence.Extensions
{
    /// <summary>
    /// THIS CLASS IS DEPRECATED. THIS APP USES AUTOMAPPER INSTEAD.
    /// This class makes the conversion between Entity and Dto classes.
    /// </summary>
    public static class EntityExtensions
    {
        public static TDto EntityToDto<TEntity, TDto>(this TEntity entity) where TDto : IDto where TEntity : IEntity
        {
            var dto = Activator.CreateInstance<TDto>();

            var dtoProperties = typeof(TDto).GetProperties();
            var entityProperties = typeof(TEntity).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                foreach (var entityProperty in entityProperties)
                {
                    if (dtoProperty.Name == entityProperty.Name)
                    {
                        dtoProperty.SetValue(dto, entityProperty.GetValue(entity));
                        break;
                    }
                }
            }

            return dto;
        }

        public static TEntity DtoToEntity<TDto, TEntity>(this TDto dto) where TDto : IDto where TEntity : IEntity
        {
            var entity = Activator.CreateInstance<TEntity>();

            var dtoProperties = typeof(TDto).GetProperties();
            var entityProperties = typeof(TEntity).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                foreach (var entityProperty in entityProperties)
                {
                    if (dtoProperty.Name == entityProperty.Name)
                    {
                        entityProperty.SetValue(entity, dtoProperty.GetValue(dto));
                        break;
                    }
                }
            }

            return entity;
        }
    }
}
