namespace Olympia.Data.Seeding
{
    using System.Collections.Generic;
    using System.Linq;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;

    public class DataSeeder
    {
        private readonly OlympiaDbContext context;

        public DataSeeder(OlympiaDbContext context)
        {
            this.context = context;

            this.SeedRoles();
            this.SeedExercises();
            this.SeedCategories();
            this.SeedArticles();
            this.SeedItems();
        }

        public void SeedRoles()
        {
            if (!this.context.Roles.Any())
            {
                this.context.Roles.Add(new OlympiaUserRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
                this.context.Roles.Add(new OlympiaUserRole { Name = "Trainer", NormalizedName = "TRAINER" });
                this.context.Roles.Add(new OlympiaUserRole { Name = "Client", NormalizedName = "CLIENT" });

                this.context.SaveChanges();
            }
        }

        public void SeedExercises()
        {
            if (!this.context.Exercises.Any())
            {
                this.context.Workouts.AddRange(new List<Workout>
                {
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Intermediate,
                        WorkoutType = WorkoutType.Strength,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Kettlebell goblet squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Back squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Bulgarian split squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Snatch-grip deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Kettlebell swing",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                     new Workout
                    {
                        Name = "Deadlift Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281092/deadlift_wgjzd0.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.Strength,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Kettlebell sumo deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Romanian deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Incline bench press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Push-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                     new Workout
                    {
                        Name = "Bench Press Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281092/bench-press_czjone.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.Strength,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Dumbbell bench press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Barbel Bench press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Diamond push-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Front squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Dumbbell jump squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                });
                this.context.SaveChanges();
            }
        }

        public void SeedCategories()
        {
            if (!this.context.ChildCategories.Any())
            {
                this.context.ChildCategories.AddRange(new List<ChildCategory>()
                {
                   new ChildCategory() { Name = "Fitness" , Description = "A category for all the items."},
                   new ChildCategory() { Name = "Clothing" , Description = "A category for all clothes."},
                   new ChildCategory() { Name = "Supplements" , Description = "A category for all the supplements."},
                });

                this.context.SaveChanges();
            }
        }

        public void SeedArticles()
        {
            if (!this.context.Articles.Any())
            {
                var articles = new List<Article>()
                {
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        Author = this.context.Users.SingleOrDefault(x => x.UserName == "God"),
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    }

                };

                this.context.Articles.AddRange(articles);
                this.context.SaveChanges();
            }
        }

        public void SeedItems()
        {
            if (!this.context.Items.Any())
            {
                var items = new List<Item>()
                {
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg"},
                };

                this.context.Items.AddRange(items);
                this.context.SaveChanges();
            }
        }
    }
}


