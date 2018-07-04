using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XYZA.Models;

namespace XYZA.BLDA
{
    public class EmailTemplateBLL
    {
        Entity _entity;

        public EmailTemplateBLL()
        {
            _entity = new Entity();
        }


        public EmailTemplate GetEmailTemplate()
        {
            return _entity.EmailTemplate.FirstOrDefault();
        }



        public bool UpdateEmailTemplate(EmailTemplate template)
        {
            try
            {
                EmailTemplate oldTemplate = GetEmailTemplate();

                if (oldTemplate == null)
                {
                    template.Id = Guid.NewGuid().ToString();
                    template.Created_Date = DateTime.UtcNow;
                    _entity.EmailTemplate.Add(template);
                }
                else
                {
                    oldTemplate.Subject = template.Subject;
                    oldTemplate.Message = template.Message;
                    oldTemplate.Remarks = template.Remarks;
                    oldTemplate.Created_Date = DateTime.UtcNow;

                    _entity.Entry(oldTemplate).State = EntityState.Modified;
                }
                _entity.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
