using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartHome.Model;

namespace SmartHome.Persistence.Repositories
{
    public class BuildingBlockRepository : IRepository<BuildingBlock>
    {
        private readonly int _maxScriptLength = 1000000;

        public async Task<bool> AddAsync(BuildingBlock newElement)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    //Checking if the script is of valid length
                    if(newElement.Script.Length > _maxScriptLength)
                    {
                        throw new ArgumentOutOfRangeException("The given script is too long to be saved!");
                    }

                    //TODO: Check if the script is a valid script!
                    
                    //TODO: Check if the script is safe to use!

                    await context.BuildingBlocks.AddAsync(newElement);

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<BuildingBlock>> FindListAsync(Expression<Func<BuildingBlock, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.BuildingBlocks.Where(queryLambda).ToListAsync();

                    //Returnning the result list.
                    //If no element found, we return null.
                    return result;
                }
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public async Task<BuildingBlock> FindAsync(Expression<Func<BuildingBlock, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.BuildingBlocks.SingleOrDefaultAsync(queryLambda);

                    //Returnning the result list.
                    //If no element found, we return null.
                    return result;
                }
            }
            catch (Exception exc)
            {
                return null;
            }
        }

        public async Task<bool> ModifyAsync(BuildingBlock toModify)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.BuildingBlocks.FirstAsync(wer => wer.Id == toModify.Id);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }

                    result = toModify;

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(BuildingBlock toDelete)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.BuildingBlocks.FirstAsync(wer => wer.Id == toDelete.Id);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }
                    context.BuildingBlocks.Remove(result);

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(Guid toRemoveId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.BuildingBlocks.FirstAsync(wer => wer.Id == toRemoveId);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }
                    context.BuildingBlocks.Remove(result);

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }
    }
}
