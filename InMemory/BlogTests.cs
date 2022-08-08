using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace InMemory;

public class BlogTests
{
    [Fact]
    public void WhenPostIsSavedThenItShouldInsertNewEntry()
    {
        // Arrange
        using (var context = new PostContext()) {

            var newPost = new Post
            {
                Id = 1,
                Title = "پست جدید",
                AuthorName = "رضا ادیبی",
                Created = DateTime.Now,
                Content = "محتوا",
                Likes = 20
            };
            
            context.Posts.Add(newPost);
            context.SaveChanges();
        }
        
        // Act
        int postsCount;
        using (var context = new PostContext()) {

            postsCount = context.Posts.Count();
        }
        
        // Assert
        postsCount.Should().Be(1);
    }
}