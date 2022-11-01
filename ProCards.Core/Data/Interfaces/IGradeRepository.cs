﻿using ProCards.DAL.Models;

namespace ProCards.Core.Data.Interfaces;

public interface IGradeRepository: IDisposable
{
    Grade GetGradeById(int gradeId);
    void InsertGrade(Grade grade);
    void Save();
}