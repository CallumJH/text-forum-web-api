using DataAccessLayer;
using DataModels;
using Interfaces;
using LinqToDB;

public class PostService : IPostService
{
    private readonly DataBaseConnection connection;

    public PostService(DataBaseConnection connection)
    {
        this.connection = connection;
    }

    public async Task<RequestWrapper<List<Post>>> GetPosts()
    {
        try
        {
            var request = new RequestWrapper<List<Post>>();
            var posts = await connection.GetTable<PostModel>().ToListAsync(); //TODO: Change this to pagination
            request.Success = true;
            request.Data = posts.Select(x => x.MapToPost()).ToList();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper<List<Post>>();
            return request;
        }
    }

    public async Task<RequestWrapper<Post>> GetPost(int id)
    {
        try
        {
            var request = new RequestWrapper<Post>();
            var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                request.Message = "Post not found";
                return request;
            }
            request.Success = true;
            request.Data = post.MapToPost();
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper<Post>();
            return request;
        }
    }

    public async Task<RequestWrapper> CreatePost(Post post)
    {
        try
        {
            var request = new RequestWrapper();
            var postModel = new PostModel(){
                Title = post.Title,
                Content = post.Content,
                CreatedByUserID = 1 //TODO: Pass up the user id from the token(SESSION WORK)
            };
            await connection.InsertAsync(postModel);
            request.Success = true;
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper();
            return request;
        }
    }

    public async Task<RequestWrapper> LikePost(int id)
    {
        try
        {
            var request = new RequestWrapper();
            var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == id);
            var like = await connection.GetTable<LikeModel>().FirstOrDefaultAsync(x => x.PostId == id && x.UserId == 1); //TODO: Pass up the user id from the token(SESSION WORK)
            if (post == null)
            {
                request.Message = "Post not found";
                return request;
            }
            if(like != null || post.CreatedByUserID == 1)
            {
                // The message expands on condition 1, condition 2 however is not mentioned if the user is trying to like a post that they created.
                request.Message = "Post already liked";
                return request;
            }

            post.LikeCount++;
            await connection.UpdateAsync(post);
            await connection.InsertAsync(new LikeModel()
            {
                PostId = id,
                UserId = 1 //TODO: Pass up the user id from the token(SESSION WORK)
            });
            request.Success = true;
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper();
            return request;
        }
    }

    public async Task<RequestWrapper> UnlikePost(int id)
    {
        try
        {
            var request = new RequestWrapper();
            var post = await connection.GetTable<PostModel>().FirstOrDefaultAsync(x => x.Id == id);
            var like = await connection.GetTable<LikeModel>().FirstOrDefaultAsync(x => x.PostId == id && x.UserId == 1); //TODO: Pass up the user id from the token(SESSION WORK)
            if (post == null)
            {
                request.Message = "Post not found";
                return request;
            }
            if (like == null || post.CreatedByUserID == 1)
            {
                // The message expands on condition 1, condition 2 however is not mentioned if the user is trying to unlike a post that they created.
                request.Message = "Post not liked cant unlike";
                return request;
            }

            post.LikeCount--;
            await connection.UpdateAsync(post);
            await connection.DeleteAsync(like); //TODO: Introduce soft delete
            request.Success = true;
            return request;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var request = new RequestWrapper();
            return request;
        }
    }

}