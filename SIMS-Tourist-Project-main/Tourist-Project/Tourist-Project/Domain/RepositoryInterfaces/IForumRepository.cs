﻿using System.Collections.Generic;
using Tourist_Project.Domain.Models;

namespace Tourist_Project.Domain.RepositoryInterfaces
{
    public interface IForumRepository
    {
        public List<Forum> GetAll();
        public Forum Save(Forum image);
        public int NextId();
        public void Delete(int id);
        public Forum Update(Forum image);
        public Forum GetById(int id);
        public Forum GetByLocationId(int id);
    }

}