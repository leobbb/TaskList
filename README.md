#软件设计-- todo列表，待办事项#

* 使用C#，SQL Server 数据库（或者XML文档）开发
* C#设计界面，SQL Server数据库（或者XML文档）存储数据
* 实现功能：
	1. 新建待办事项
	2. 显示当前事项的列表
	3. 标记已完成
	4. 查看待办事项历史

> 使用的开发环境为 Visual Studio 2013

> 依赖： .NET 4.5， XML 1.0.

## 源码有三个分支(branches)：master.using-sql, using-XML.##
* master: 初始的分支，内容等于using-XML分支。
* using-sql: 使用SQL Server LocalDb 2012数据库存储数据，实现上述功能。
* using-XML: 使用XML 文档存储数据，实现上述功能。


## 使用说明：##
* 在任务列表中通过双击对任务进行操作 
	* 当”正在进行中“按钮选中时，双击任务可以把任务状态标记为”done"
	* 当“查看已完成”按钮选中时，双击任务可以把任务状态恢复为“doing”
* 通过输入框输入任务内容，点击”添加“，即可添加到XML（或者SQL Server）中，并在列表中显示
* 分别单击”正在进行中“和”查看已完成“按钮可以分别浏览正在进行中的任务和已完成的任务
* 正在进行中的任务显示的顺序为，后添加的任务在下边
* 已完成的任务显示的顺序为，后完成的任务在上边

