﻿// <auto-generated />
using System;
using HackerNews.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackerNews.EF.Migrations
{
    [DbContext(typeof(HackerNewsContext))]
    [Migration("20200927232901_user-saves")]
    partial class usersaves
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HackerNews.Domain.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Karma")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("HackerNews.Domain.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Karma")
                        .HasColumnType("int");

                    b.Property<int?>("ParentArticleId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentArticleId");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("HackerNews.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Karma")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HackerNews.Domain.UserArticle", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("UserArticle");
                });

            modelBuilder.Entity("HackerNews.Domain.UserComment", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("UserComment");
                });

            modelBuilder.Entity("HackerNews.Domain.Article", b =>
                {
                    b.HasOne("HackerNews.Domain.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HackerNews.Domain.Comment", b =>
                {
                    b.HasOne("HackerNews.Domain.Article", "ParentArticle")
                        .WithMany("Comments")
                        .HasForeignKey("ParentArticleId");

                    b.HasOne("HackerNews.Domain.Comment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("HackerNews.Domain.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HackerNews.Domain.UserArticle", b =>
                {
                    b.HasOne("HackerNews.Domain.Article", "Article")
                        .WithMany("UsersSaved")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackerNews.Domain.User", "User")
                        .WithMany("SavedArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HackerNews.Domain.UserComment", b =>
                {
                    b.HasOne("HackerNews.Domain.Comment", "Comment")
                        .WithMany("UsersSaved")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HackerNews.Domain.User", "User")
                        .WithMany("SavedComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}