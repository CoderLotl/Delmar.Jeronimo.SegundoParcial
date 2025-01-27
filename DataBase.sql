USE [master]
GO
/****** Object:  Database [Parcial]    Script Date: 11/24/2022 6:39:06 PM ******/
CREATE DATABASE [Parcial]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Parcial', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SERVIDORPARCIAL\MSSQL\DATA\Parcial.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Parcial_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SERVIDORPARCIAL\MSSQL\DATA\Parcial_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Parcial] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Parcial].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Parcial] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Parcial] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Parcial] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Parcial] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Parcial] SET ARITHABORT OFF 
GO
ALTER DATABASE [Parcial] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Parcial] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Parcial] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Parcial] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Parcial] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Parcial] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Parcial] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Parcial] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Parcial] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Parcial] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Parcial] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Parcial] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Parcial] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Parcial] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Parcial] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Parcial] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Parcial] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Parcial] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Parcial] SET  MULTI_USER 
GO
ALTER DATABASE [Parcial] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Parcial] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Parcial] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Parcial] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Parcial] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Parcial]
GO
/****** Object:  Table [dbo].[Matches]    Script Date: 11/24/2022 6:39:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Matches](
	[Game] [nvarchar](500) NULL,
	[GameLog] [nvarchar](max) NULL,
	[Date] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Players]    Script Date: 11/24/2022 6:39:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Players](
	[PlayerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Players] PRIMARY KEY CLUSTERED 
(
	[PlayerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 11/24/2022 6:39:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[ID] [int] NOT NULL,
	[GamesPlayed] [int] NOT NULL,
	[GamesWon] [int] NOT NULL,
	[GamesLost] [int] NOT NULL,
	[GamesTied] [int] NOT NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Matches] ([Game], [GameLog], [Date]) VALUES (N'Room #1 - Game type Cards - Game sub-type Truco | Players: Jessica-BOT - Nathan-BOT | Date: 11/24/2022 3:39:30 PM', N'{\rtf1\ansi \b Round 1\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 11 \b0 of \b Golds\b0, the \b 2 \b0 of \b Golds\b0, and the \b 3 \b0 of \b Golds \b0.\line\line \b Jessica-BOT \b0 got 3 cards.\line The \b 5 \b0 of \b Golds\b0, the \b 7 \b0 of \b Clubs\b0, and the \b 12 \b0 of \b Swords \b0.\line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 2 \b0 of \b Golds \b0\line\line \b Jessica-BOT\b0: Envido!\line\line \b Nathan-BOT\b0: No quiero!\line\line \b +1pt for Jessica-BOT\b0\line\line\b Jessica-BOT \b0 plays the \b 5 \b0 of \b Golds \b0\line\line The \b 2 \b0 of \b Golds \b0 kills the \b 5 \b0 of \b Golds. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line \b Nathan-BOT\b0: Truco!\line\line \b Jessica-BOT\b0: Quiero!\line\line\b Nathan-BOT \b0 plays the \b 3 \b0 of \b Golds \b0\line\line\b Jessica-BOT \b0 plays the \b 12 \b0 of \b Swords \b0\line\line The \b 3 \b0 of \b Golds \b0 kills the \b 12 \b0 of \b Swords. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line \b Nathan-BOT wins this round.\b0\line\line \b +2pt for Nathan-BOT\b0\line\line----------------------------------------------------------- \line \b Round 2\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Jessica-BOT \b0 got 3 cards.\line The \b 12 \b0 of \b Swords\b0, the \b 4 \b0 of \b Golds\b0, and the \b 2 \b0 of \b Clubs \b0.\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 7 \b0 of \b Swords\b0, the \b 11 \b0 of \b Clubs\b0, and the \b 10 \b0 of \b Swords \b0.\line\line* * * * * * * * * * \line\b Jessica-BOT \b0 plays the \b 2 \b0 of \b Clubs \b0\line\line \b Nathan-BOT\b0: Envido!\line\line \b Jessica-BOT\b0: Quiero!\line\line \b Jessica-BOT\b0:  4  \line\line \b Nathan-BOT\b0:  4  \line\line \b Jessica-BOT wins since they''re Hand.\b0\line\line \b +1pt for Jessica-BOT\b0\line\line* * * * *  \line \b Nathan-BOT\b0: Truco!\line\line \b Jessica-BOT\b0: Quiero!\line\line\b Nathan-BOT \b0 plays the \b 7 \b0 of \b Swords \b0\line\line The \b 7 \b0 of \b Swords \b0 kills the \b 2 \b0 of \b Clubs. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 10 \b0 of \b Swords \b0\line\line\b Jessica-BOT \b0 plays the \b 12 \b0 of \b Swords \b0\line\line The \b 12 \b0 of \b Swords \b0 kills the \b 10 \b0 of \b Swords. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 11 \b0 of \b Clubs \b0\line\line\b Jessica-BOT \b0 plays the \b 4 \b0 of \b Golds \b0\line\line The \b 11 \b0 of \b Clubs \b0 kills the \b 4 \b0 of \b Golds. \b0\line\line Jessica-BOT is hand now. \line\line* * * * * * * * * * \line \b Jessica-BOT wins this round.\b0\line\line \b +2pt for Jessica-BOT\b0\line\line----------------------------------------------------------- \line \b Round 3\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 10 \b0 of \b Cups\b0, the \b 6 \b0 of \b Swords\b0, and the \b 3 \b0 of \b Swords \b0.\line\line \b Jessica-BOT \b0 got 3 cards.\line The \b 4 \b0 of \b Clubs\b0, the \b 6 \b0 of \b Cups\b0, and the \b 2 \b0 of \b Cups \b0.\line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 3 \b0 of \b Swords \b0\line\line \b Jessica-BOT\b0: Envido!\line\line \b Nathan-BOT\b0: No quiero!\line\line \b +1pt for Jessica-BOT\b0\line\line \b Jessica-BOT\b0: Truco!\line\line \b Nathan-BOT\b0: Quiero!\line\line\b Jessica-BOT \b0 plays the \b 2 \b0 of \b Cups \b0\line\line The \b 3 \b0 of \b Swords \b0 kills the \b 2 \b0 of \b Cups. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 10 \b0 of \b Cups \b0\line\line\b Jessica-BOT \b0 plays the \b 6 \b0 of \b Cups \b0\line\line The \b 10 \b0 of \b Cups \b0 kills the \b 6 \b0 of \b Cups. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line \b Nathan-BOT wins this round.\b0\line\line \b +2pt for Nathan-BOT\b0\line\line----------------------------------------------------------- \line \b Round 4\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 10 \b0 of \b Swords\b0, the \b 3 \b0 of \b Cups\b0, and the \b 3 \b0 of \b Clubs \b0.\line\line \b Jessica-BOT \b0 got 3 cards.\line The \b 7 \b0 of \b Golds\b0, the \b 7 \b0 of \b Cups\b0, and the \b 1 \b0 of \b Golds \b0.\line\line* * * * * * * * * * \line \b Nathan-BOT\b0: Envido!\line\line \b Jessica-BOT\b0: No quiero!\line\line \b +1pt for Nathan-BOT\b0\line\line\b Nathan-BOT \b0 plays the \b 3 \b0 of \b Clubs \b0\line\line\b Jessica-BOT \b0 plays the \b 1 \b0 of \b Golds \b0\line\line The \b 3 \b0 of \b Clubs \b0 kills the \b 1 \b0 of \b Golds. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 3 \b0 of \b Cups \b0\line\line\b Jessica-BOT \b0 plays the \b 7 \b0 of \b Cups \b0\line\line The \b 3 \b0 of \b Cups \b0 kills the \b 7 \b0 of \b Cups. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line \b Nathan-BOT wins this round.\b0\line\line \b +1pt for Nathan-BOT\b0\line\line----------------------------------------------------------- \line \b\i Game Finished. - - - Please wait...\i0\b0\line\line \b Jessica-BOT is the Winner!\b0\line \b\i Updating players statics...\i0\b0\line\line}', CAST(N'2022-11-24T15:39:30.530' AS DateTime))
INSERT [dbo].[Matches] ([Game], [GameLog], [Date]) VALUES (N'Room #1 - Game type Cards - Game sub-type Truco | Players: Rob-BOT - Mauro-BOT | Date: 11/24/2022 5:25:27 PM', N'{\rtf1\ansi \b Round 1\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Rob-BOT \b0 got 3 cards.\line The \b 5 \b0 of \b Swords\b0, the \b 7 \b0 of \b Cups\b0, and the \b 6 \b0 of \b Cups \b0.\line\line \b Mauro-BOT \b0 got 3 cards.\line The \b 2 \b0 of \b Cups\b0, the \b 4 \b0 of \b Golds\b0, and the \b 4 \b0 of \b Cups \b0.\line\line* * * * * * * * * * \line \b Rob-BOT\b0: Envido!\line\line \b Mauro-BOT\b0: No quiero!\line\line \b +1pt for Rob-BOT\b0\line\line\b Rob-BOT \b0 plays the \b 6 \b0 of \b Cups \b0\line\line \b Mauro-BOT\b0: Truco!\line\line \b Rob-BOT\b0: No quiero!\line\line \b +1pt for Mauro-BOT\b0\line\line \b Mauro-BOT wins this round.\b0\line\line \b +1pt for Mauro-BOT\b0\line\line----------------------------------------------------------- \line \b Round 2\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Mauro-BOT \b0 got 3 cards.\line The \b 6 \b0 of \b Clubs\b0, the \b 1 \b0 of \b Cups\b0, and the \b 6 \b0 of \b Cups \b0.\line\line \b Rob-BOT \b0 got 3 cards.\line The \b 7 \b0 of \b Golds\b0, the \b 2 \b0 of \b Swords\b0, and the \b 2 \b0 of \b Cups \b0.\line\line* * * * * * * * * * \line\b Mauro-BOT \b0 plays the \b 6 \b0 of \b Clubs \b0\line\line \b Rob-BOT\b0: Envido!\line\line \b Mauro-BOT\b0: Quiero!\line\line \b Mauro-BOT\b0:  27  \line\line \b Rob-BOT\b0:  27  \line\line \b Mauro-BOT wins since they''re Hand.\b0\line\line \b +1pt for Mauro-BOT\b0\line\line* * * * *  \line \b Rob-BOT\b0: Truco!\line\line \b Mauro-BOT\b0: No quiero!\line\line \b +1pt for Rob-BOT\b0\line\line \b Rob-BOT wins this round.\b0\line\line \b +1pt for Rob-BOT\b0\line\line----------------------------------------------------------- \line \b Round 3\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Rob-BOT \b0 got 3 cards.\line The \b 2 \b0 of \b Cups\b0, the \b 5 \b0 of \b Golds\b0, and the \b 4 \b0 of \b Cups \b0.\line\line \b Mauro-BOT \b0 got 3 cards.\line The \b 12 \b0 of \b Cups\b0, the \b 4 \b0 of \b Clubs\b0, and the \b 3 \b0 of \b Swords \b0.\line\line* * * * * * * * * * \line\b Rob-BOT \b0 plays the \b 4 \b0 of \b Cups \b0\line\line\b Mauro-BOT \b0 plays the \b 3 \b0 of \b Swords \b0\line\line The \b 3 \b0 of \b Swords \b0 kills the \b 4 \b0 of \b Cups. \b0\line\line Mauro-BOT is hand now. \line\line* * * * * * * * * * \line\b Mauro-BOT \b0 plays the \b 12 \b0 of \b Cups \b0\line\line\b Rob-BOT \b0 plays the \b 5 \b0 of \b Golds \b0\line\line The \b 12 \b0 of \b Cups \b0 kills the \b 5 \b0 of \b Golds. \b0\line\line Rob-BOT is hand now. \line\line* * * * * * * * * * \line \b Rob-BOT wins this round.\b0\line\line \b +1pt for Rob-BOT\b0\line\line----------------------------------------------------------- \line \b Round 4\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Mauro-BOT \b0 got 3 cards.\line The \b 4 \b0 of \b Cups\b0, the \b 2 \b0 of \b Cups\b0, and the \b 3 \b0 of \b Clubs \b0.\line\line \b Rob-BOT \b0 got 3 cards.\line The \b 3 \b0 of \b Cups\b0, the \b 12 \b0 of \b Swords\b0, and the \b 4 \b0 of \b Clubs \b0.\line\line* * * * * * * * * * \line\b Mauro-BOT \b0 plays the \b 4 \b0 of \b Cups \b0\line\line \b Rob-BOT\b0: Envido!\line\line \b Mauro-BOT\b0: No quiero!\line\line \b +1pt for Rob-BOT\b0\line\line\b Rob-BOT \b0 plays the \b 12 \b0 of \b Swords \b0\line\line The \b 12 \b0 of \b Swords \b0 kills the \b 4 \b0 of \b Cups. \b0\line\line Rob-BOT is hand now. \line\line* * * * * * * * * * \line\b Rob-BOT \b0 plays the \b 4 \b0 of \b Clubs \b0\line\line \b Mauro-BOT\b0: Truco!\line\line Rob-BOT wins this one since they''re Hand. \line\line* * * * * * * * * * \line \b Rob-BOT\b0: Quiero!\line\line\b Mauro-BOT \b0 plays the \b 2 \b0 of \b Cups \b0\line\line The \b 2 \b0 of \b Cups \b0 kills the \b 4 \b0 of \b Clubs. \b0\line\line Rob-BOT is hand now. \line\line* * * * * * * * * * \line \b Rob-BOT wins this round.\b0\line\line \b +2pt for Rob-BOT\b0\line\line----------------------------------------------------------- \line \b\i Game Finished. - - - Please wait...\i0\b0\line\line \b Both players ended in ties.\b0\line \b\i Updating players statics...\i0\b0\line\line}', CAST(N'2022-11-24T17:25:27.663' AS DateTime))
INSERT [dbo].[Matches] ([Game], [GameLog], [Date]) VALUES (N'Room #2 - Game type Cards - Game sub-type Truco | Players: asd - Nathan-BOT | Date: 11/24/2022 5:28:28 PM', N'{\rtf1\ansi \b Round 1\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 2 \b0 of \b Clubs\b0, the \b 7 \b0 of \b Clubs\b0, and the \b 5 \b0 of \b Swords \b0.\line\line \b asd \b0 got 3 cards.\line The \b 12 \b0 of \b Cups\b0, the \b 4 \b0 of \b Clubs\b0, and the \b 3 \b0 of \b Cups \b0.\line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 2 \b0 of \b Clubs \b0\line\line \b asd\b0: Envido!\line\line \b Nathan-BOT\b0: No quiero!\line\line \b +1pt for asd\b0\line\line \b asd\b0: Truco!\line\line \b Nathan-BOT\b0: No quiero!\line\line \b +1pt for asd\b0\line\line \b asd wins this round.\b0\line\line \b +1pt for asd\b0\line\line----------------------------------------------------------- \line \b Round 2\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 7 \b0 of \b Clubs\b0, the \b 6 \b0 of \b Swords\b0, and the \b 10 \b0 of \b Golds \b0.\line\line \b asd \b0 got 3 cards.\line The \b 7 \b0 of \b Swords\b0, the \b 11 \b0 of \b Golds\b0, and the \b 6 \b0 of \b Cups \b0.\line\line* * * * * * * * * * \line \b Nathan-BOT\b0: Envido!\line\line \b asd\b0: No quiero!\line\line \b +1pt for Nathan-BOT\b0\line\line\b Nathan-BOT \b0 plays the \b 10 \b0 of \b Golds \b0\line\line\b asd \b0 plays the \b 7 \b0 of \b Swords \b0\line\line The \b 7 \b0 of \b Swords \b0 kills the \b 10 \b0 of \b Golds. \b0\line\line asd is hand now. \line\line* * * * * * * * * * \line\b asd \b0 plays the \b 11 \b0 of \b Golds \b0\line\line\b Nathan-BOT \b0 plays the \b 7 \b0 of \b Clubs \b0\line\line The \b 11 \b0 of \b Golds \b0 kills the \b 7 \b0 of \b Clubs. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line \b Nathan-BOT wins this round.\b0\line\line \b +1pt for Nathan-BOT\b0\line\line----------------------------------------------------------- \line \b Round 3\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 2 \b0 of \b Clubs\b0, the \b 4 \b0 of \b Cups\b0, and the \b 11 \b0 of \b Cups \b0.\line\line \b asd \b0 got 3 cards.\line The \b 1 \b0 of \b Swords\b0, the \b 7 \b0 of \b Swords\b0, and the \b 1 \b0 of \b Cups \b0.\line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 2 \b0 of \b Clubs \b0\line\line\b asd \b0 plays the \b 1 \b0 of \b Cups \b0\line\line The \b 2 \b0 of \b Clubs \b0 kills the \b 1 \b0 of \b Cups. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line \b Nathan-BOT\b0: Envido!\line\line \b asd\b0: No quiero!\line\line \b +1pt for Nathan-BOT\b0\line\line\b Nathan-BOT \b0 plays the \b 11 \b0 of \b Cups \b0\line\line \b asd\b0: Truco!\line\line The \b 1 \b0 of \b Cups \b0 kills the \b 11 \b0 of \b Cups. \b0\line\line asd is hand now. \line\line* * * * * * * * * * \line \b asd\b0: No quiero!\line\line \b +1pt for Nathan-BOT\b0\line\line \b Nathan-BOT wins this round.\b0\line\line \b +1pt for Nathan-BOT\b0\line\line\b asd \b0 plays the \b 1 \b0 of \b Swords \b0\line\line----------------------------------------------------------- \line \b Round 4\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b asd \b0 got 3 cards.\line The \b 6 \b0 of \b Clubs\b0, the \b 11 \b0 of \b Golds\b0, and the \b 5 \b0 of \b Swords \b0.\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 10 \b0 of \b Golds\b0, the \b 7 \b0 of \b Cups\b0, and the \b 2 \b0 of \b Swords \b0.\line\line* * * * * * * * * * \line\b asd \b0 plays the \b 11 \b0 of \b Golds \b0\line\line \b Nathan-BOT\b0: Envido!\line\line \b asd\b0: No quiero!\line\line \b +1pt for Nathan-BOT\b0\line\line\b Nathan-BOT \b0 plays the \b 7 \b0 of \b Cups \b0\line\line The \b 11 \b0 of \b Golds \b0 kills the \b 7 \b0 of \b Cups. \b0\line\line asd is hand now. \line\line* * * * * * * * * * \line\b asd \b0 plays the \b 6 \b0 of \b Clubs \b0\line\line\b Nathan-BOT \b0 plays the \b 2 \b0 of \b Swords \b0\line\line The \b 2 \b0 of \b Swords \b0 kills the \b 6 \b0 of \b Clubs. \b0\line\line Nathan-BOT is hand now. \line\line* * * * * * * * * * \line\b Nathan-BOT \b0 plays the \b 10 \b0 of \b Golds \b0\line\line \b asd\b0: Truco!\line\line The \b 10 \b0 of \b Golds \b0 kills the \b 6 \b0 of \b Clubs. \b0\line\line asd is hand now. \line\line* * * * * * * * * * \line \b asd wins this round.\b0\line\line \b +1pt for asd\b0\line\line----------------------------------------------------------- \line \b\i Game Finished. - - - Please wait...\i0\b0\line\line \b Nathan-BOT is the Winner!\b0\line \b\i Updating players statics...\i0\b0\line\line}', CAST(N'2022-11-24T17:28:28.607' AS DateTime))
INSERT [dbo].[Matches] ([Game], [GameLog], [Date]) VALUES (N'Room #1 - Game type Cards - Game sub-type Truco | Players: Mauro-BOT - Nathan-BOT | Date: 11/24/2022 5:33:07 PM', N'{\rtf1\ansi \b Round 1\b0.\line\line \b Shuffling the deck... \b0\line\line \b Giving cards... \b0\line\line \b Mauro-BOT \b0 got 3 cards.\line The \b 1 \b0 of \b Golds\b0, the \b 10 \b0 of \b Clubs\b0, and the \b 3 \b0 of \b Cups \b0.\line\line \b Nathan-BOT \b0 got 3 cards.\line The \b 3 \b0 of \b Clubs\b0, the \b 12 \b0 of \b Clubs\b0, and the \b 2 \b0 of \b Swords \b0.\line\line* * * * * * * * * * \line\b Mauro-BOT \b0 plays the \b 3 \b0 of \b Cups \b0\line\line \b Nathan-BOT\b0: Envido!\line\line \b Mauro-BOT\b0: No quiero!\line\line \b +1pt for Nathan-BOT\b0\line\line\b Nathan-BOT \b0 plays the \b 2 \b0 of \b Swords \b0\line\line The \b 3 \b0 of \b Cups \b0 kills the \b 2 \b0 of \b Swords. \b0\line\line \line\b\i [ Finishing the game after this round... ] \b0\i0\line\line Mauro-BOT is hand now. \line\line* * * * * * * * * * \line----------------------------------------------------------- \line \b\i Game Finished. - - - Please wait...\i0\b0\line\line \b Mauro-BOT \b0 score is: 0 \line \b Nathan-BOT \b0 score is: 1 \line \b Nathan-BOT is the Winner!\b0\line \b\i Updating players statics...\i0\b0\line\line}', CAST(N'2022-11-24T17:33:07.543' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Players] ON 

INSERT [dbo].[Players] ([PlayerID], [Name]) VALUES (1, N'Rob-BOT')
INSERT [dbo].[Players] ([PlayerID], [Name]) VALUES (2, N'Jessica-BOT')
INSERT [dbo].[Players] ([PlayerID], [Name]) VALUES (4, N'Mauro-BOT')
INSERT [dbo].[Players] ([PlayerID], [Name]) VALUES (6, N'Nathan-BOT')
SET IDENTITY_INSERT [dbo].[Players] OFF
GO
INSERT [dbo].[Score] ([ID], [GamesPlayed], [GamesWon], [GamesLost], [GamesTied]) VALUES (1, 1, 0, 0, 1)
INSERT [dbo].[Score] ([ID], [GamesPlayed], [GamesWon], [GamesLost], [GamesTied]) VALUES (2, 1, 1, 0, 0)
INSERT [dbo].[Score] ([ID], [GamesPlayed], [GamesWon], [GamesLost], [GamesTied]) VALUES (4, 2, 0, 1, 1)
INSERT [dbo].[Score] ([ID], [GamesPlayed], [GamesWon], [GamesLost], [GamesTied]) VALUES (6, 3, 2, 1, 0)
GO
USE [master]
GO
ALTER DATABASE [Parcial] SET  READ_WRITE 
GO
