using SmartHomeWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SmartHome_WebApp.Data.Repositories
{
    public class DesignRepository : IRepository<Design>
    {
        public async Task<bool> Add(Design newElement)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
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

        public async Task<List<Design>> Find(Expression<Func<Design, bool>> queryLambda)
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

        public async Task<bool> Modify(Design toModify)
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

        public async Task<bool> Remove(Design toDelete)
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

        public async Task<bool> Remove(Guid toRemoveId)
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
