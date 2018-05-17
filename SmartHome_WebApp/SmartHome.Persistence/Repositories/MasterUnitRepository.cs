using Microsoft.EntityFrameworkCore;
using SmartHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Persistence.Repositories
{
    public class MasterUnitRepository : IRepository<MasterUnit>
    {
        public async Task<bool> AddAsync(MasterUnit newElement)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var user = await context.Users.SingleOrDefaultAsync(wer => wer.UserName == newElement.Owner.UserName);
                    if(user == null)
                    {
                        return false;
                    }
                    newElement.Owner = user;
                    await context.MasterUnits.AddAsync(newElement);
                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() < 1)
                    {
                        throw new ApplicationException("Saving data was uncusseful!");
                    }
                }
                return true;
            }

            catch (ApplicationException exc)
            {
                return false;
            }
            catch (DbUpdateException exc)
            {
                return false;
            }
        }

        public async Task<List<MasterUnit>> FindListAsync(Expression<Func<MasterUnit, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.MasterUnits.Where(queryLambda).ToListAsync();

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

        public async Task<MasterUnit> FindAsync(Expression<Func<MasterUnit, bool>> queryLambda)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.MasterUnits.SingleOrDefaultAsync(queryLambda);

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


        public async Task<bool> ModifyAsync(MasterUnit toModify)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.MasterUnits.FirstAsync(f => 
                    f.Id == toModify.Id
                    && f.Owner.Id == toModify.Owner.Id
                    );

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }

                    result = toModify;

                    //Checking if the saving method succeeds
                    if (await context.SaveChangesAsync() >= 1)
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

        public async Task<bool> RemoveAsync(MasterUnit toDelete)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var result = await context.MasterUnits.FirstAsync(wer => toDelete.Id == wer.Id && toDelete.Owner.Id == wer.Owner.Id);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }
                    context.MasterUnits.Remove(result);

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
                    var result = await context.MasterUnits.FirstAsync(wer => wer.Id == toRemoveId);

                    //Check if the element we need is i  the database
                    if (result == null)
                    {
                        throw new NullReferenceException("No member like the given parameter was present in the DB");
                    }

                    context.MasterUnits.Remove(result);

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
