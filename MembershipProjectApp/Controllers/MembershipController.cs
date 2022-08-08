using MembershipProjectApp.Data;
using MembershipProjectApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipProjectApp.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MembershipController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> MemberList()
        {
            var sql = await _db.Memberships.ToListAsync();
            ViewBag.TotalMembers = _db.Memberships.Count();
            ViewBag.TotalUsers = _db.AspNetUsers.Count();
            ViewBag.FoundingMembers = _db.Memberships.Count(x => x.Membership == "Founding Member");
            return View(sql);
        }

        // DETAILS METHOD
        public async Task<IActionResult> MemberDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _db.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (membershipModel == null)
            {
                return NotFound();
            }

            return View(membershipModel);
        }

        // CREATE METHODS         
        public IActionResult CreateMember()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMember([Bind("MemberId,FirstName,MiddleName,LastName,Contact,Email,Occupation,Education,CountyOfOrigin,CountyOfResidence,Address,Membership,Category,Gender,Position,MaritalStatus,Ethnicity,Language,Mosque,IDNumber,ReceiptNumber,PreRegistration,Registration,Payment,Balance,Person,NumberOfPerson")] MembershipModel membershipModel)
        {
            if (ModelState.IsValid)
            {
                membershipModel.RecordedBy = User.Identity.Name;
                membershipModel.DateRecorded = DateTime.Now;
                _db.Add(membershipModel);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(MemberList));
            }
            return View(membershipModel);
        }

        // UPDATE METHODS
        public async Task<IActionResult> EditMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _db.Memberships.FindAsync(id);
            if (membershipModel == null)
            {
                return NotFound();
            }
            return View(membershipModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMember(int id, [Bind("MemberId,FirstName,MiddleName,LastName,Contact,Email,Occupation,Education,CountyOfOrigin,CountyOfResidence,Address,Membership,Category,Gender,Position,MaritalStatus,Ethnicity,Language,Mosque,IDNumber,ReceiptNumber,PreRegistration,Registration,Payment,Balance,Person,NumberOfPerson")] MembershipModel membershipModel)
        {
            if (id != membershipModel.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(membershipModel);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipModelExists(membershipModel.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MemberList));
            }
            return View(membershipModel);
        }


        // DELETE METHODS
        public async Task<IActionResult> DeleteMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _db.Memberships
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (membershipModel == null)
            {
                return NotFound();
            }

            return View(membershipModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int MemberId)
        {
            var membershipModel = await _db.Memberships.FindAsync(MemberId);
            _db.Memberships.Remove(membershipModel);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(MemberList));
        }
        

        [HttpGet]
        public async Task<IActionResult> PrintPage()
        {
            var sql = await _db.Memberships.ToListAsync();
            ViewBag.Logo = "https://scontent.xx.fbcdn.net/v/t1.15752-9/280191816_762260518096253_3839266920817166461_n.jpg?stp=dst-jpg_s206x206&_nc_cat=106&ccb=1-6&_nc_sid=aee45a&_nc_eui2=AeFg-PD2JMfeS_l1OctUVN--IDn60skmo3QgOfrSySajdOj9d4kpwqOTZEnyM3nReDaNapdQ5qSBzGnVcLD8vlPT&_nc_ohc=q1ciaBjotK0AX_XVYCQ&_nc_ad=z-m&_nc_cid=0&_nc_ht=scontent.xx&oh=03_AVKsKsrJxHWZmuyj1pV7rwbbUCEr-VqogWOSf3XPPUxuJQ&oe=62A3CB8E";
            ViewBag.FlagLogo = "https://us.123rf.com/450wm/istanbul2009/istanbul20091503/istanbul2009150300189/37268888-vector-image-map-with-dot-pattern-on-flag-button-of-republic-of-liberia.jpg?ver=6";
            return View(sql);
        }

        [HttpGet]
        public async Task<IActionResult> ExcelPage()
        {
            var sql = await _db.Memberships.ToListAsync();
            return View(sql);
        }


        private bool MembershipModelExists(int id)
        {
            return _db.Memberships.Any(e => e.MemberId == id);
        }
    }
}
