namespace Shop.Core.ServiceInterface;

using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEmailService
{
    Task SendEmail(EmailDto dto);
}
