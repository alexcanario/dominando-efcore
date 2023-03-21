using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projeto1.Domain;

namespace Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{

}