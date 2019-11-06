USE [DTRDB]
GO
/****** Object:  Table [dbo].[BlogComment]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogComment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [varchar](20) NOT NULL,
	[BlogWorksID] [int] NOT NULL,
	[CommentContent] [varchar](200) NOT NULL,
	[CommentDate] [datetime] NULL,
	[ReadState] [bit] NULL,
	[ParentCommentID] [int] NULL,
 CONSTRAINT [PK_BlogComment_1] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogStateType]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogStateType](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogType]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogType](
	[BlogTypeID] [int] IDENTITY(1,1) NOT NULL,
	[BlogTypeName] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogUser]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogUser](
	[BlogID] [varchar](20) NOT NULL,
	[BlogPwd] [varchar](20) NOT NULL,
	[Nikename] [varchar](30) NOT NULL,
	[FrozenState] [bit] NOT NULL,
	[StartFrozenDate] [datetime] NULL,
	[EndFrozenDate] [datetime] NULL,
	[FrozenReason] [nvarchar](200) NULL,
	[UnHonour] [int] NULL,
	[UserPath] [nvarchar](200) NOT NULL,
	[SupportCount] [int] NULL,
	[FollowCount] [int] NULL,
	[CollectionCount] [int] NULL,
	[CommentCount] [int] NULL,
	[Sex] [bit] NULL,
	[RegisterTime] [datetime] NULL,
	[Email] [nvarchar](40) NULL,
	[Birthday] [datetime] NULL,
	[Area] [varchar](50) NULL,
	[Industry] [varchar](50) NULL,
	[Position] [varchar](40) NULL,
	[BlogDese] [varchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogWorks]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogWorks](
	[BlogWorksID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [varchar](20) NULL,
	[StateID] [int] NULL,
	[BlogTypeID] [int] NULL,
	[SingleBlogTypeID] [int] NULL,
	[Title] [varchar](60) NOT NULL,
	[Introduc] [nvarchar](500) NULL,
	[PublishDate] [datetime] NULL,
	[TopState] [bit] NULL,
	[OpenState] [bit] NULL,
	[CharLength] [int] NULL,
	[SupportCount] [int] NULL,
	[FollowCount] [int] NULL,
	[CommentCount] [int] NULL,
	[BorwseCount] [int] NULL,
	[WorksPath] [nvarchar](200) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Collection]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collection](
	[CollectionID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [varchar](20) NOT NULL,
	[BlogWorksID] [int] NOT NULL,
	[CollectionDate] [datetime] NULL,
 CONSTRAINT [PK_Collection_1] PRIMARY KEY CLUSTERED 
(
	[CollectionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Follow]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Follow](
	[FollowID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [varchar](20) NOT NULL,
	[TargetBlogID] [varchar](20) NOT NULL,
	[FollowDate] [datetime] NULL,
 CONSTRAINT [PK_Follow_1] PRIMARY KEY CLUSTERED 
(
	[FollowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [int] IDENTITY(1,1) NOT NULL,
	[AccepteBlogID] [varchar](20) NULL,
	[NewsTypeID] [int] NULL,
	[SourceBlogID] [varchar](20) NULL,
	[AdminID] [varchar](20) NULL,
	[NewsContent] [varchar](200) NOT NULL,
	[NewsDate] [datetime] NULL,
	[ReadState] [bit] NULL,
	[URL] [varchar](200) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsType]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsType](
	[NewsTypeID] [int] IDENTITY(1,1) NOT NULL,
	[NewsTypeName] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SingleBlogType]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SingleBlogType](
	[SingleBlogTypeID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [varchar](20) NOT NULL,
	[SingleBlogTypeName] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Support]    Script Date: 2019/10/26 18:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Support](
	[SupportID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [varchar](20) NOT NULL,
	[BlogWorksID] [int] NOT NULL,
	[SupportDate] [datetime] NULL,
 CONSTRAINT [PK_Support_1] PRIMARY KEY CLUSTERED 
(
	[SupportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BlogComment] ON 

INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (1, N'100000', 204, N'666', CAST(N'2019-10-10T15:18:53.307' AS DateTime), 0, 0)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (2, N'100001', 204, N'6666', CAST(N'2019-10-10T19:37:30.617' AS DateTime), 0, 0)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (3, N'100000', 206, N'666', CAST(N'2019-10-16T13:47:04.583' AS DateTime), 0, 0)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (4, N'100000', 206, N' 好', CAST(N'2019-10-16T13:47:15.743' AS DateTime), 0, 3)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (5, N'100001', 207, N'666', CAST(N'2019-10-16T14:27:51.317' AS DateTime), 0, 0)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (6, N'100003', 208, N'666', CAST(N'2019-10-25T14:22:21.907' AS DateTime), 0, 0)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (7, N'100003', 208, N' 666', CAST(N'2019-10-25T14:22:33.363' AS DateTime), 0, 6)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (8, N'100002', 208, N' 222', CAST(N'2019-10-25T14:51:36.443' AS DateTime), 0, 6)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (9, N'100002', 210, N'是吗？', CAST(N'2019-10-25T14:54:49.253' AS DateTime), 0, 0)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (10, N'100002', 210, N' 嘿嘿', CAST(N'2019-10-25T14:55:01.717' AS DateTime), 0, 9)
INSERT [dbo].[BlogComment] ([CommentID], [BlogID], [BlogWorksID], [CommentContent], [CommentDate], [ReadState], [ParentCommentID]) VALUES (11, N'123456', 208, N'666', CAST(N'2019-10-26T11:38:09.890' AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[BlogComment] OFF
SET IDENTITY_INSERT [dbo].[BlogStateType] ON 

INSERT [dbo].[BlogStateType] ([StateID], [StateName]) VALUES (1, N'草稿')
INSERT [dbo].[BlogStateType] ([StateID], [StateName]) VALUES (2, N'未审核')
INSERT [dbo].[BlogStateType] ([StateID], [StateName]) VALUES (3, N'审核通过')
INSERT [dbo].[BlogStateType] ([StateID], [StateName]) VALUES (4, N'审核未通过')
SET IDENTITY_INSERT [dbo].[BlogStateType] OFF
SET IDENTITY_INSERT [dbo].[BlogType] ON 

INSERT [dbo].[BlogType] ([BlogTypeID], [BlogTypeName]) VALUES (6, N'生活')
INSERT [dbo].[BlogType] ([BlogTypeID], [BlogTypeName]) VALUES (2, N'动漫')
INSERT [dbo].[BlogType] ([BlogTypeID], [BlogTypeName]) VALUES (3, N'音乐')
INSERT [dbo].[BlogType] ([BlogTypeID], [BlogTypeName]) VALUES (8, N'影视')
INSERT [dbo].[BlogType] ([BlogTypeID], [BlogTypeName]) VALUES (5, N'技术宅')
INSERT [dbo].[BlogType] ([BlogTypeID], [BlogTypeName]) VALUES (10, N'综合')
SET IDENTITY_INSERT [dbo].[BlogType] OFF
INSERT [dbo].[BlogUser] ([BlogID], [BlogPwd], [Nikename], [FrozenState], [StartFrozenDate], [EndFrozenDate], [FrozenReason], [UnHonour], [UserPath], [SupportCount], [FollowCount], [CollectionCount], [CommentCount], [Sex], [RegisterTime], [Email], [Birthday], [Area], [Industry], [Position], [BlogDese]) VALUES (N'100000', N'123456', N'SnailClimb', 0, NULL, NULL, NULL, NULL, N'DTRBlogUser\024207b1-69a9-466d-99af-cb4983486856\', NULL, NULL, NULL, NULL, 1, CAST(N'2019-10-10T15:12:21.973' AS DateTime), NULL, CAST(N'2019-09-21T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
INSERT [dbo].[BlogUser] ([BlogID], [BlogPwd], [Nikename], [FrozenState], [StartFrozenDate], [EndFrozenDate], [FrozenReason], [UnHonour], [UserPath], [SupportCount], [FollowCount], [CollectionCount], [CommentCount], [Sex], [RegisterTime], [Email], [Birthday], [Area], [Industry], [Position], [BlogDese]) VALUES (N'100002', N'123456', N'才华横溢', 0, NULL, NULL, NULL, NULL, N'DTRBlogUser\49178a39-f673-4df3-bcef-e4efb57bcb90\', NULL, NULL, NULL, NULL, 1, CAST(N'2019-10-10T16:38:06.453' AS DateTime), NULL, CAST(N'2019-10-10T00:00:00.000' AS DateTime), NULL, N'印刷·包装·造纸', NULL, NULL)
INSERT [dbo].[BlogUser] ([BlogID], [BlogPwd], [Nikename], [FrozenState], [StartFrozenDate], [EndFrozenDate], [FrozenReason], [UnHonour], [UserPath], [SupportCount], [FollowCount], [CollectionCount], [CommentCount], [Sex], [RegisterTime], [Email], [Birthday], [Area], [Industry], [Position], [BlogDese]) VALUES (N'100001', N'123456', N'Jenny', 0, NULL, NULL, NULL, NULL, N'DTRBlogUser\85a254f0-ff76-43ef-92ac-fcba88d5c6df\', NULL, NULL, NULL, NULL, NULL, CAST(N'2019-10-10T19:25:45.920' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[BlogUser] ([BlogID], [BlogPwd], [Nikename], [FrozenState], [StartFrozenDate], [EndFrozenDate], [FrozenReason], [UnHonour], [UserPath], [SupportCount], [FollowCount], [CollectionCount], [CommentCount], [Sex], [RegisterTime], [Email], [Birthday], [Area], [Industry], [Position], [BlogDese]) VALUES (N'100003', N'123456', N'小三', 0, NULL, NULL, NULL, NULL, N'DTRBlogUser\838bdc42-46fe-415e-b59e-6dc160b8964c\', NULL, NULL, NULL, NULL, NULL, CAST(N'2019-10-25T14:21:47.293' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[BlogUser] ([BlogID], [BlogPwd], [Nikename], [FrozenState], [StartFrozenDate], [EndFrozenDate], [FrozenReason], [UnHonour], [UserPath], [SupportCount], [FollowCount], [CollectionCount], [CommentCount], [Sex], [RegisterTime], [Email], [Birthday], [Area], [Industry], [Position], [BlogDese]) VALUES (N'2333', N'123456', N'2333', 0, NULL, NULL, NULL, NULL, N'DTRBlogUser\efd8c2b0-8256-46bd-8e41-678db2f59a5d\', NULL, NULL, NULL, NULL, NULL, CAST(N'2019-10-26T10:55:12.020' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[BlogUser] ([BlogID], [BlogPwd], [Nikename], [FrozenState], [StartFrozenDate], [EndFrozenDate], [FrozenReason], [UnHonour], [UserPath], [SupportCount], [FollowCount], [CollectionCount], [CommentCount], [Sex], [RegisterTime], [Email], [Birthday], [Area], [Industry], [Position], [BlogDese]) VALUES (N'123456', N'123456', N'123456', 0, NULL, NULL, NULL, NULL, N'DTRBlogUser\00367cb9-31e1-4957-9ba9-343363478598\', NULL, NULL, NULL, NULL, NULL, CAST(N'2019-10-26T11:37:36.777' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[BlogWorks] ON 

INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (204, N'100000', 3, NULL, NULL, N'新手也能看懂的 SpringBoot 异步编程指南', N'异步编程在处理耗时操作以及多任务处理的场景下非常有用，我们可以更好的让我们的系统利用好机器的 CPU 和 内存，提高它们的利用率。多线程设计模式有很多种，Future模式是多线程开发中非常常见的一种设计模式，本文也是基于这种模式来说明 SpringBoot 对于异步编程的知识。

作者：SnailClimb
链接：https://juejin.im/post/5d9e7cfa6fb9a04e1f12ec02
来源：掘金
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。', CAST(N'2019-10-10T15:18:30.787' AS DateTime), 0, 1, 9485, 0, 0, 0, 6, N'DTRBlogUser\024207b1-69a9-466d-99af-cb4983486856\BlogWorks\2019-10-10 03-13-07\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (208, N'100002', 3, 6, NULL, N'肯德基为什么不卖一整只鸡？网友全部买来拼在一起，价格真相了！', N'说到“肯德基”不说人人都吃过吧，基本上每个家庭都有人吃过，尤其的小孩子可以说从90后到10后的小孩子都吃过，肯德基被我们中国人称为是“洋人餐厅”，但是它在中国的影响力还是非常大的，可以说从一线城市到三四线城市，都能见到肯德基的餐厅，先不说开一家肯德基需要多少的加盟费用，只要是有肯德基的地方，基本上都是这个城市的黄金码头。', CAST(N'2019-10-10T17:13:40.847' AS DateTime), 0, 1, 744, 1, 0, 0, 11, N'DTRBlogUser\49178a39-f673-4df3-bcef-e4efb57bcb90\BlogWorks\2019-10-10 05-08-56\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (206, N'100000', 3, 5, NULL, N'Kotlin修炼指南', N'作用域函数
作用域函数是Kotlin中的一个非常有用的函数，它主要分为两种，一种是拓展函数式，另一种是顶层函数式。作用域函数的主要功能是为调用函数提供一个内部范围，同时结合kotlin的语法糖提供一些便捷操作。', CAST(N'2019-10-10T15:39:29.733' AS DateTime), 0, 1, 6360, 0, 0, 0, 5, N'DTRBlogUser\024207b1-69a9-466d-99af-cb4983486856\BlogWorks\2019-10-10 03-37-17\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (207, N'100000', 3, 5, NULL, N'Vue3响应式系统源码解析(上)', N'从reactivity的入口文件进去，发现它只是暴露了6个文件内的apis。分别是： ref 、reactive 、computed 、effect 、lock 、operations 。其中 lock 跟 operations 很简单， lock 文件内部就是两个控制锁开关变量的方法， operations 内部就是对数据操作的类型的枚举。

作者：蚂蚁保险体验技术
链接：https://juejin.im/post/5d9c9a135188252e097569bd
来源：掘金
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。', CAST(N'2019-10-10T15:44:10.973' AS DateTime), 0, 1, 23019, 1, 0, 0, 6, N'DTRBlogUser\024207b1-69a9-466d-99af-cb4983486856\BlogWorks\2019-10-10 03-40-34\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (209, N'100002', 3, 5, NULL, N'使用 GitHub 的十个最佳实践', N'保护主分支，不要在其上直接提交代码
主分支中的任何内容都应该是可部署的，所以不应该直接在默认分支上提交代码，而且 Gitflow 工作流已成为标准。使用分支保护可以防止直接提交代码，当然，所有内容都应该通过 pr 进行管理。', CAST(N'2019-10-10T17:16:03.687' AS DateTime), 0, 1, 1541, 0, 0, 0, 18, N'DTRBlogUser\49178a39-f673-4df3-bcef-e4efb57bcb90\BlogWorks\2019-10-10 05-14-42\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (210, N'100002', 3, NULL, NULL, N'我一直都是那样的人', N'等有一天，你找到了你所谓的爱人，你们会结婚，会生子，会携手到老，你们会相互忍让，为孩子，为家庭。

等有一天，我也找到了我所谓的爱人，我也会结婚，会生子，也会跟她携手到老，也会因为种种想法不同而吵架，甚至会打架。', CAST(N'2019-10-10T17:19:57.613' AS DateTime), 0, 1, 514, 0, 0, 0, 5, N'DTRBlogUser\49178a39-f673-4df3-bcef-e4efb57bcb90\BlogWorks\2019-10-10 05-18-31\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (212, N'100001', 1, NULL, NULL, N'一个新的博客', NULL, CAST(N'2019-10-10T19:36:24.380' AS DateTime), 0, 1, 44, 0, 0, 0, 0, N'DTRBlogUser\85a254f0-ff76-43ef-92ac-fcba88d5c6df\BlogWorks\2019-10-10 07-35-27\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (213, N'100000', 1, NULL, NULL, N'一个新的博客', NULL, CAST(N'2019-10-16T20:01:05.460' AS DateTime), 0, 1, 0, 0, 0, 0, 0, N'DTRBlogUser\024207b1-69a9-466d-99af-cb4983486856\BlogWorks\2019-10-16 08-01-03\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (211, N'100002', 3, NULL, NULL, N'异性之间，要想难以分开，共享这些就行了.', N'两个人走到一起是很容易的，要想一直在一起却很难。明天是一个未知数，会发生什么事情，会让一个人有着怎么样的改变，我们永远不会知道。

热恋中的情侣，会珍惜每一分每一秒，常对彼此说“不管未来的路有多难走，我们都要大步跨过去”。可热恋期一过，就不这么想了，觉得这句话不现实。

一辈子说长不长，说短不短，主要看你怎样生活，和谁一起度过。真正的爱情，会很幸福，觉得每一天都不够用，很想让时间慢一点。', CAST(N'2019-10-10T17:24:13.663' AS DateTime), 0, 1, 1577, 0, 0, 0, 6, N'DTRBlogUser\49178a39-f673-4df3-bcef-e4efb57bcb90\BlogWorks\2019-10-10 05-21-42\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (214, N'100000', 3, 10, 8, N'111111111111111', N'1111111111111', CAST(N'2019-10-26T11:09:30.643' AS DateTime), 0, 1, 70, 0, 0, 0, 0, N'DTRBlogUser\024207b1-69a9-466d-99af-cb4983486856\BlogWorks\2019-10-26 11-08-46\')
INSERT [dbo].[BlogWorks] ([BlogWorksID], [BlogID], [StateID], [BlogTypeID], [SingleBlogTypeID], [Title], [Introduc], [PublishDate], [TopState], [OpenState], [CharLength], [SupportCount], [FollowCount], [CommentCount], [BorwseCount], [WorksPath]) VALUES (215, N'123456', 3, NULL, NULL, N'wwwww', N'111111', CAST(N'2019-10-26T11:38:55.477' AS DateTime), 0, 1, 70, 0, 0, 0, 0, N'DTRBlogUser\00367cb9-31e1-4957-9ba9-343363478598\BlogWorks\2019-10-26 11-38-16\')
SET IDENTITY_INSERT [dbo].[BlogWorks] OFF
SET IDENTITY_INSERT [dbo].[Collection] ON 

INSERT [dbo].[Collection] ([CollectionID], [BlogID], [BlogWorksID], [CollectionDate]) VALUES (9, N'100001', 211, CAST(N'2019-10-16T14:09:40.777' AS DateTime))
INSERT [dbo].[Collection] ([CollectionID], [BlogID], [BlogWorksID], [CollectionDate]) VALUES (10, N'100001', 206, CAST(N'2019-10-16T14:09:48.723' AS DateTime))
INSERT [dbo].[Collection] ([CollectionID], [BlogID], [BlogWorksID], [CollectionDate]) VALUES (11, N'100001', 207, CAST(N'2019-10-16T14:27:39.447' AS DateTime))
INSERT [dbo].[Collection] ([CollectionID], [BlogID], [BlogWorksID], [CollectionDate]) VALUES (12, N'123456', 208, CAST(N'2019-10-26T11:38:02.997' AS DateTime))
SET IDENTITY_INSERT [dbo].[Collection] OFF
SET IDENTITY_INSERT [dbo].[Follow] ON 

INSERT [dbo].[Follow] ([FollowID], [BlogID], [TargetBlogID], [FollowDate]) VALUES (15, N'100000', N'100002', CAST(N'2019-10-16T14:06:16.613' AS DateTime))
INSERT [dbo].[Follow] ([FollowID], [BlogID], [TargetBlogID], [FollowDate]) VALUES (16, N'100000', N'100001', CAST(N'2019-10-16T14:06:23.297' AS DateTime))
INSERT [dbo].[Follow] ([FollowID], [BlogID], [TargetBlogID], [FollowDate]) VALUES (17, N'123456', N'100002', CAST(N'2019-10-26T11:38:00.243' AS DateTime))
SET IDENTITY_INSERT [dbo].[Follow] OFF
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (1, N'100000', 1, N'100000', NULL, N'666', CAST(N'2019-10-10T15:18:53.307' AS DateTime), 0, N'204')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (2, N'100000', 2, N'100001', NULL, N'关注了你', CAST(N'2019-10-10T19:35:01.403' AS DateTime), 0, NULL)
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (3, N'100000', 1, N'100001', NULL, N'6666', CAST(N'2019-10-10T19:37:30.617' AS DateTime), 0, N'204')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (4, N'100000', 1, N'100000', NULL, N'666', CAST(N'2019-10-16T13:47:04.583' AS DateTime), 0, N'206')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (5, N'100000', 5, N'100000', NULL, N' 好', CAST(N'2019-10-16T13:47:15.747' AS DateTime), 0, N'206')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (6, N'100002', 2, N'100000', NULL, N'关注了你', CAST(N'2019-10-16T14:06:16.613' AS DateTime), 0, NULL)
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (7, N'100001', 2, N'100000', NULL, N'关注了你', CAST(N'2019-10-16T14:06:23.297' AS DateTime), 0, NULL)
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (8, N'100002', 4, N'100001', NULL, N'收藏了你的作品', CAST(N'2019-10-16T14:09:40.770' AS DateTime), 0, N'211')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (9, N'100000', 4, N'100001', NULL, N'收藏了你的作品', CAST(N'2019-10-16T14:09:48.720' AS DateTime), 0, N'206')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (10, N'100000', 3, N'100001', NULL, N'点赞了你的作品', CAST(N'2019-10-16T14:27:38.347' AS DateTime), 0, N'207')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (11, N'100000', 4, N'100001', NULL, N'收藏了你的作品', CAST(N'2019-10-16T14:27:39.447' AS DateTime), 0, N'207')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (12, N'100000', 1, N'100001', NULL, N'666', CAST(N'2019-10-16T14:27:51.317' AS DateTime), 0, N'207')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (13, N'100002', 1, N'100003', NULL, N'666', CAST(N'2019-10-25T14:22:21.907' AS DateTime), 0, N'208')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (14, N'100002', 5, N'100003', NULL, N' 666', CAST(N'2019-10-25T14:22:33.367' AS DateTime), 0, N'208')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (15, N'100003', 5, N'100002', NULL, N' 222', CAST(N'2019-10-25T14:51:37.010' AS DateTime), 0, N'208')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (16, N'100002', 1, N'100002', NULL, N'是吗？', CAST(N'2019-10-25T14:54:49.253' AS DateTime), 0, N'210')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (17, N'100002', 5, N'100002', NULL, N' 嘿嘿', CAST(N'2019-10-25T14:55:01.740' AS DateTime), 0, N'210')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (18, N'100002', 2, N'123456', NULL, N'关注了你', CAST(N'2019-10-26T11:38:00.243' AS DateTime), 0, NULL)
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (19, N'100002', 3, N'123456', NULL, N'点赞了你的作品', CAST(N'2019-10-26T11:38:02.143' AS DateTime), 0, N'208')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (20, N'100002', 4, N'123456', NULL, N'收藏了你的作品', CAST(N'2019-10-26T11:38:02.997' AS DateTime), 0, N'208')
INSERT [dbo].[News] ([NewsID], [AccepteBlogID], [NewsTypeID], [SourceBlogID], [AdminID], [NewsContent], [NewsDate], [ReadState], [URL]) VALUES (21, N'100002', 1, N'123456', NULL, N'666', CAST(N'2019-10-26T11:38:09.890' AS DateTime), 0, N'208')
SET IDENTITY_INSERT [dbo].[News] OFF
SET IDENTITY_INSERT [dbo].[NewsType] ON 

INSERT [dbo].[NewsType] ([NewsTypeID], [NewsTypeName]) VALUES (1, N'评论')
INSERT [dbo].[NewsType] ([NewsTypeID], [NewsTypeName]) VALUES (2, N'关注')
INSERT [dbo].[NewsType] ([NewsTypeID], [NewsTypeName]) VALUES (3, N'点赞')
INSERT [dbo].[NewsType] ([NewsTypeID], [NewsTypeName]) VALUES (4, N'收藏')
INSERT [dbo].[NewsType] ([NewsTypeID], [NewsTypeName]) VALUES (5, N'回答')
INSERT [dbo].[NewsType] ([NewsTypeID], [NewsTypeName]) VALUES (6, N'系统')
SET IDENTITY_INSERT [dbo].[NewsType] OFF
SET IDENTITY_INSERT [dbo].[SingleBlogType] ON 

INSERT [dbo].[SingleBlogType] ([SingleBlogTypeID], [BlogID], [SingleBlogTypeName]) VALUES (8, N'100000', N'我的文章')
SET IDENTITY_INSERT [dbo].[SingleBlogType] OFF
SET IDENTITY_INSERT [dbo].[Support] ON 

INSERT [dbo].[Support] ([SupportID], [BlogID], [BlogWorksID], [SupportDate]) VALUES (9, N'100001', 207, CAST(N'2019-10-16T14:27:38.350' AS DateTime))
INSERT [dbo].[Support] ([SupportID], [BlogID], [BlogWorksID], [SupportDate]) VALUES (10, N'123456', 208, CAST(N'2019-10-26T11:38:02.143' AS DateTime))
SET IDENTITY_INSERT [dbo].[Support] OFF
/****** Object:  Index [PK_BLOGSTATETYPE]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[BlogStateType] ADD  CONSTRAINT [PK_BLOGSTATETYPE] PRIMARY KEY NONCLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_BLOGTYPE]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[BlogType] ADD  CONSTRAINT [PK_BLOGTYPE] PRIMARY KEY NONCLUSTERED 
(
	[BlogTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [PK_BLOGUSER]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [PK_BLOGUSER] PRIMARY KEY NONCLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_BLOGWORKS]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [PK_BLOGWORKS] PRIMARY KEY NONCLUSTERED 
(
	[BlogWorksID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_NEWS]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [PK_NEWS] PRIMARY KEY NONCLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_NEWSTYPE]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[NewsType] ADD  CONSTRAINT [PK_NEWSTYPE] PRIMARY KEY NONCLUSTERED 
(
	[NewsTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_SINGLEBLOGTYPE]    Script Date: 2019/10/26 18:42:37 ******/
ALTER TABLE [dbo].[SingleBlogType] ADD  CONSTRAINT [PK_SINGLEBLOGTYPE] PRIMARY KEY NONCLUSTERED 
(
	[SingleBlogTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [DF_BlogUser_FrozenState]  DEFAULT ((0)) FOR [FrozenState]
GO
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [DF_BlogUser_UnHonour]  DEFAULT ((0)) FOR [UnHonour]
GO
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [DF_BlogUser_SupporCount]  DEFAULT ((0)) FOR [SupportCount]
GO
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [DF_BlogUser_FollowCount]  DEFAULT ((0)) FOR [FollowCount]
GO
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [DF_BlogUser_CollectionCount]  DEFAULT ((0)) FOR [CollectionCount]
GO
ALTER TABLE [dbo].[BlogUser] ADD  CONSTRAINT [DF_BlogUser_CommentCount]  DEFAULT ((0)) FOR [CommentCount]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_TopState]  DEFAULT ((0)) FOR [TopState]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_OpenState]  DEFAULT ((1)) FOR [OpenState]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_CharLength]  DEFAULT ((0)) FOR [CharLength]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_SupportCount]  DEFAULT ((0)) FOR [SupportCount]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_FollowCount]  DEFAULT ((0)) FOR [FollowCount]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_CommentCount]  DEFAULT ((0)) FOR [CommentCount]
GO
ALTER TABLE [dbo].[BlogWorks] ADD  CONSTRAINT [DF_BlogWorks_BorwseCount]  DEFAULT ((0)) FOR [BorwseCount]
GO
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_ReadState]  DEFAULT ((0)) FOR [ReadState]
GO
ALTER TABLE [dbo].[BlogComment]  WITH CHECK ADD  CONSTRAINT [FK_BLOGCOMM_BLOGCOMME_BLOGWORK] FOREIGN KEY([BlogWorksID])
REFERENCES [dbo].[BlogWorks] ([BlogWorksID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogComment] CHECK CONSTRAINT [FK_BLOGCOMM_BLOGCOMME_BLOGWORK]
GO
ALTER TABLE [dbo].[BlogWorks]  WITH CHECK ADD  CONSTRAINT [FK_BLOGWORK_CONTAIN_BLOGSTAT] FOREIGN KEY([StateID])
REFERENCES [dbo].[BlogStateType] ([StateID])
GO
ALTER TABLE [dbo].[BlogWorks] CHECK CONSTRAINT [FK_BLOGWORK_CONTAIN_BLOGSTAT]
GO
ALTER TABLE [dbo].[BlogWorks]  WITH CHECK ADD  CONSTRAINT [FK_BLOGWORK_CONTAIN1_BLOGTYPE] FOREIGN KEY([BlogTypeID])
REFERENCES [dbo].[BlogType] ([BlogTypeID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[BlogWorks] CHECK CONSTRAINT [FK_BLOGWORK_CONTAIN1_BLOGTYPE]
GO
ALTER TABLE [dbo].[BlogWorks]  WITH CHECK ADD  CONSTRAINT [FK_BLOGWORK_CONTAIN4_SINGLEBL] FOREIGN KEY([SingleBlogTypeID])
REFERENCES [dbo].[SingleBlogType] ([SingleBlogTypeID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[BlogWorks] CHECK CONSTRAINT [FK_BLOGWORK_CONTAIN4_SINGLEBL]
GO
ALTER TABLE [dbo].[BlogWorks]  WITH CHECK ADD  CONSTRAINT [FK_BLOGWORK_PUBLISH_BLOGUSER] FOREIGN KEY([BlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogWorks] CHECK CONSTRAINT [FK_BLOGWORK_PUBLISH_BLOGUSER]
GO
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_COLLECTI_COLLECTIO_BLOGUSER] FOREIGN KEY([BlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_COLLECTI_COLLECTIO_BLOGUSER]
GO
ALTER TABLE [dbo].[Collection]  WITH CHECK ADD  CONSTRAINT [FK_COLLECTI_COLLECTIO_BLOGWORK] FOREIGN KEY([BlogWorksID])
REFERENCES [dbo].[BlogWorks] ([BlogWorksID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Collection] CHECK CONSTRAINT [FK_COLLECTI_COLLECTIO_BLOGWORK]
GO
ALTER TABLE [dbo].[Follow]  WITH CHECK ADD  CONSTRAINT [FK_FOLLOW_AllFOLLOW_BLOGUSER] FOREIGN KEY([BlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[Follow] CHECK CONSTRAINT [FK_FOLLOW_AllFOLLOW_BLOGUSER]
GO
ALTER TABLE [dbo].[Follow]  WITH CHECK ADD  CONSTRAINT [FK_FOLLOW_TargetFOLLOW_BLOGUSER] FOREIGN KEY([TargetBlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[Follow] CHECK CONSTRAINT [FK_FOLLOW_TargetFOLLOW_BLOGUSER]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_NEWS_ SendPRODUCE_BLOGUSER] FOREIGN KEY([SourceBlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_NEWS_ SendPRODUCE_BLOGUSER]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_NEWS_AccepteRECEIVED_BLOGUSER] FOREIGN KEY([AccepteBlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_NEWS_AccepteRECEIVED_BLOGUSER]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_NEWS_CONTAIN6_NEWSTYPE] FOREIGN KEY([NewsTypeID])
REFERENCES [dbo].[NewsType] ([NewsTypeID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_NEWS_CONTAIN6_NEWSTYPE]
GO
ALTER TABLE [dbo].[SingleBlogType]  WITH CHECK ADD  CONSTRAINT [FK_SINGLEBL_CREATE_BLOGUSER] FOREIGN KEY([BlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[SingleBlogType] CHECK CONSTRAINT [FK_SINGLEBL_CREATE_BLOGUSER]
GO
ALTER TABLE [dbo].[Support]  WITH CHECK ADD  CONSTRAINT [FK_SUPPORT_SUPPORT_BLOGWORK] FOREIGN KEY([BlogWorksID])
REFERENCES [dbo].[BlogWorks] ([BlogWorksID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Support] CHECK CONSTRAINT [FK_SUPPORT_SUPPORT_BLOGWORK]
GO
ALTER TABLE [dbo].[Support]  WITH CHECK ADD  CONSTRAINT [FK_SUPPORT_SUPPORT2_BLOGUSER] FOREIGN KEY([BlogID])
REFERENCES [dbo].[BlogUser] ([BlogID])
GO
ALTER TABLE [dbo].[Support] CHECK CONSTRAINT [FK_SUPPORT_SUPPORT2_BLOGUSER]
GO
