using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SmartHome.Model;

namespace SmartHome.Persistence.Repositories
{
    public class DesignRepository : IRepository<Design>
    {
        public async Task<bool> AddAsync(Design newElement)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var user = await context.Users.SingleOrDefaultAsync(wer => wer.UserName == newElement.Creator.UserName);
                    if (user == null)
                    {
                        return false;
                    }
                    newElement.Creator = user;

                    var userResult = await context.Users.FindAsync(newElement.Creator.Id);

                    //Checking if the referenced User exists
                    if(userResult == null)
                    {
                        throw new NullReferenceException($"The User {newElement.Creator.UserName} referenced by the parameter does not exist!");
                    }

                    /*
                     * We must check if this desing is made out of valid Building blocks.
                     * To check that we need to check that every member of its Sequence
                     * property exists in the database.
                     */
                    foreach (var item in newElement.Sequence)
                    {
                        var result = await context.BuildingBlocks.FindAsync(item.Id);
                        if(result == null)
                        {
                            throw new NullReferenceException($"The BuldingBlock referenced by the parameters property: {nameof(newElement.Sequence)} does not exist!");
                        }
                    }

                    await context.Designs.AddAsync(newElement);

                    //Checking if the save is succefull
                    if(await context.SaveChangesAsync() != 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<List<Design>> FindListAsync(Expression<Func<Design, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                   return await context.Designs.Where(queryLambda).ToListAsync();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Design> FindAsync(Expression<Func<Design, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.Designs.SingleOrDefaultAsync(queryLambda);

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

        public async Task<bool> ModifyAsync(Design toModify)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.Designs.FirstAsync(wer => wer.Id == toModify.Id);

                    //Check if the resuired entry exists in the DB
                    if(result == null)
                    {
                        throw new NullReferenceException($"The required entry does not exist in the DB!");
                    }

                    result = toModify;
                    //Checking if the save is succefull
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

        public async Task<bool> RemoveAsync(Design toDelete)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.Designs.FirstAsync(wer => wer.Id == toDelete.Id);

                    //Check if the resuired entry exists in the DB
                    if (result == null)
                    {
                        throw new NullReferenceException($"The required entry does not exist in the DB!");
                    }

                    context.Designs.Remove(result);

                    //Checking if the save is succefull
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

        public async Task<bool> RemoveAsync(Guid toRemoveId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.Designs.FirstAsync(wer => wer.Id == toRemoveId);

                    //Check if the resuired entry exists in the DB
                    if (result == null)
                    {
                        throw new NullReferenceException($"The required entry does not exist in the DB!");
                    }

                    context.Designs.Remove(result);

                    //Checking if the save is succefull
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
    }
}
