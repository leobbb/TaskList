/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     8/27/2014 9:33:06 AM                         */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('TaskList')
            and   type = 'U')
   drop table TaskList
go

/*==============================================================*/
/* Table: Task                                                  */
/*==============================================================*/
create table TaskList (
   taskId               int                  not null identity ,
   taskContent          char(50)             not null,
   taskStatus           bit               null default 0,
   timeNew              datetime             null,
   timaDone             datetime             null,
   constraint PK_TASK primary key nonclustered (taskId)
)
go

