using HealthChecker.Data.Definitions;
using System;

namespace HealthChecker.Data.Extensions
{
    /// <summary>
    /// This extension is no longer being used. AutoMapper is being used instead of this implementation.
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
