namespace Olympia.Data.Seeding
{
    using Microsoft.EntityFrameworkCore;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TestSeeder
    {
        private readonly OlympiaDbContext context;

        public TestSeeder(OlympiaDbContext context)
        {
            this.context = context;
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();

            this.SeedRolesAsync().GetAwaiter().GetResult();
            this.SeedWorkoutAndExercises().GetAwaiter().GetResult();
            this.SeedCategories().GetAwaiter().GetResult();
            this.SeedArticles().GetAwaiter().GetResult();
            this.SeedSuppliers().GetAwaiter().GetResult();
            this.SeedItems().GetAwaiter().GetResult();
        }

        private async Task SeedRolesAsync()
        {
            await this.context.Roles.AddAsync(new OlympiaUserRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
            await this.context.Roles.AddAsync(new OlympiaUserRole { Name = "Trainer", NormalizedName = "TRAINER" });
            await this.context.Roles.AddAsync(new OlympiaUserRole { Name = "Client", NormalizedName = "CLIENT" });

            this.context.Users.AddRange(new List<OlympiaUser>()
                {
                    new OlympiaUser
                    {
                        UserName = "root",
                        FullName = "root root",
                        PasswordHash = "123123",
                    },
                    new OlympiaUser
                    {
                        UserName = "Pesho",
                        FullName = "Pesho Petrov",
                        PasswordHash = "adasdas"
                    },
                    new OlympiaUser
                    {
                        UserName = "Mesho",
                        FullName = "Pesho Petrov",
                        PasswordHash = "adasdas"
                    },
                    new OlympiaUser
                    {
                        UserName = "Gesho",
                        FullName = "Pesho Petrov",
                        PasswordHash = "adasdas"
                    }
                });

            await this.context.SaveChangesAsync();

        }

        private async Task SeedWorkoutAndExercises()
        {
            var strengthWokouts = new List<Workout>
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
                };
            var circuitWorkouts = new List<Workout>
                {
                    new Workout
                    {
                        Name = "Circuit Endurance",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.Circuit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Jump rope: 2-3 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping jacks: 25 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Body weight squats: 20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Lunges: 5 reps each leg.",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip rotations: 5 each leg ",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forward leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Side leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Push ups: 10-20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Spider-man steps: 10 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "Circuit Endurance",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.Circuit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Jump rope: 2-3 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping jacks: 25 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Body weight squats: 20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Lunges: 5 reps each leg.",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip rotations: 5 each leg ",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forward leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Side leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Push ups: 10-20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Spider-man steps: 10 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "Circuit Endurance",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Intermediate,
                        WorkoutType = WorkoutType.Circuit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Jump rope: 2-3 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping jacks: 25 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Body weight squats: 20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Lunges: 5 reps each leg.",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip rotations: 5 each leg ",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forward leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Side leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Push ups: 10-20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Spider-man steps: 10 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "Circuit Endurance",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.Circuit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Jump rope: 2-3 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping jacks: 25 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Body weight squats: 20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Lunges: 5 reps each leg.",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip rotations: 5 each leg ",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forward leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Side leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Push ups: 10-20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Spider-man steps: 10 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "Circuit Endurance",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.Circuit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Jump rope: 2-3 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping jacks: 25 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Body weight squats: 20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Lunges: 5 reps each leg.",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip rotations: 5 each leg ",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Hip extensions: 10 reps each side",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forward leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Side leg swings: 10 each leg",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Push ups: 10-20 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Spider-man steps: 10 reps",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                        }
                    },
                };
            var crossfitWorkouts = new List<Workout>
                {
                    new Workout
                    {
                        Name = "CrossFit Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.CrossFit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Air Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Front Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Shoulder Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Push Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Wall Ball",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Pull-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "CrossFit Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Intermediate,
                        WorkoutType = WorkoutType.CrossFit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Air Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Front Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Shoulder Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Push Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Wall Ball",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Pull-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "CrossFit Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.CrossFit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Air Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Front Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Shoulder Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Push Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Wall Ball",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Pull-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "CrossFit Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.CrossFit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Air Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Front Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Shoulder Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Push Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Wall Ball",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Pull-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },
                    new Workout
                    {
                        Name = "CrossFit Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Intermediate,
                        WorkoutType = WorkoutType.CrossFit,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Air Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Front Squat",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Shoulder Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 4,
                            },
                            new Exercise
                            {
                                Name = "Push Press",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Deadlift",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Wall Ball",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                            new Exercise
                            {
                                Name = "Pull-up",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 3,
                            },
                        }
                    },

                };
            var cardioWorkouts = new List<Workout>
                {
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.Cardio,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Running: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cycling: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cross-Thranjor: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },

                        }
                    },
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.Cardio,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Running: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cycling: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cross-Thranjor: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },

                        }
                    },
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.Cardio,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Running: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cycling: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cross-Thranjor: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },

                        }
                    },
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.Cardio,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Running: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cycling: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },
                            new Exercise
                            {
                                Name = "Cross-Thranjor: 20 minutes",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 2,
                            },

                        }
                    },
                };
            var hiitWorkouts = new List<Workout>
                {
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.HIIT,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Butt Kicks — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jump Squats — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Burpees — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Mountain Climbers — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Alternating Side Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forearm Plank — 20-second hold",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                             new Exercise
                            {
                                Name = "Mountain Climbers — 40 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                             },
                            new Exercise
                            {
                                Name = "Plank Jacks — 40 secondss",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },

                            },
                    },
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Intermediate,
                        WorkoutType = WorkoutType.HIIT,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Butt Kicks — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jump Squats — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Burpees — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Mountain Climbers — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Alternating Side Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forearm Plank — 20-second hold",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                             new Exercise
                            {
                                Name = "Mountain Climbers — 40 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                             },
                            new Exercise
                            {
                                Name = "Plank Jacks — 40 secondss",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },

                            },
                    },
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Advanced,
                        WorkoutType = WorkoutType.HIIT,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Butt Kicks — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jump Squats — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Burpees — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Mountain Climbers — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Alternating Side Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forearm Plank — 20-second hold",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                             new Exercise
                            {
                                Name = "Mountain Climbers — 40 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                             },
                            new Exercise
                            {
                                Name = "Plank Jacks — 40 secondss",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },

                            },
                    },
                    new Workout
                    {
                        Name = "Legs Strength",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563281091/back-squat_i8awxc.jpg",
                        WorkoutDifficulty = WorkoutDifficulty.Beginners,
                        WorkoutType = WorkoutType.HIIT,
                        Exercises = new HashSet<Exercise>
                        {
                            new Exercise
                            {
                                Name = "Butt Kicks — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jump Squats — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Burpees — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Mountain Climbers — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Alternating Side Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Jumping Lunges — 45 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                            new Exercise
                            {
                                Name = "Forearm Plank — 20-second hold",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },
                             new Exercise
                            {
                                Name = "Mountain Climbers — 40 seconds",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                             },
                            new Exercise
                            {
                                Name = "Plank Jacks — 40 secondss",
                                TargetBodyPart = BodyPart.Legs,
                                Sets = 1,
                            },

                            },
                    },
                };

            await this.context.Workouts.AddRangeAsync(strengthWokouts);
            await this.context.Workouts.AddRangeAsync(circuitWorkouts);
            await this.context.Workouts.AddRangeAsync(crossfitWorkouts);
            await this.context.Workouts.AddRangeAsync(cardioWorkouts);
            await this.context.Workouts.AddRangeAsync(hiitWorkouts);
            await this.context.SaveChangesAsync();
        }

        private async Task SeedCategories()
        {
            await this.context.ChildCategories.AddRangeAsync(new List<ChildCategory>()
                {
                   new ChildCategory() { Name = "Fitness" , Description = "A category for all the items."},
                   new ChildCategory() { Name = "Clothing" , Description = "A category for all clothes."},
                   new ChildCategory() { Name = "Supplements" , Description = "A category for all the supplements."},
                });

            await this.context.SaveChangesAsync();
        }

        private async Task SeedArticles()
        {
            var user = await this.context.Users.SingleOrDefaultAsync(x => x.UserName == "Pesho");
            var articles = new List<Article>()
                {
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg",
                        TimesRead = 4
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg",
                        TimesRead = 4
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "How to get stronger today",
                        Content = "orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563984082/eojtgytpm1a0i1q0katk.jpg",
                        TimesRead = 4
                    }

                };

            await this.context.Articles.AddRangeAsync(articles);
            await this.context.SaveChangesAsync();
        }

        private async Task SeedSuppliers()
        {
            var suppliers = new List<Supplier>()
                {
                    new Supplier
                    {
                        Name = "GymBeam",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "Bodybuilding",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "MaxPower",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "Olympia",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "IronIide",
                        Description = "The best supplier ever."
                    },
                    new Supplier
                    {
                        Name = "Thunder",
                        Description = "The best supplier ever."
                    }
                };

            await this.context.Suppliers.AddRangeAsync(suppliers);
            await this.context.SaveChangesAsync();

        }
        private async Task SeedItems()
        {
            var fitnessCategory = await this.context.ChildCategories.SingleOrDefaultAsync(cat => cat.Name == "Fitness");
            var clothingCategory = await this.context.ChildCategories.SingleOrDefaultAsync(cat => cat.Name == "Clothing");
            var supplementsCategory = await this.context.ChildCategories.SingleOrDefaultAsync(cat => cat.Name == "Supplements");

            var supplier = await this.context.Suppliers.SingleOrDefaultAsync(supp => supp.Name == "GymBeam");

            var items = new List<Item>()
                {
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1563565242/prhnarsdb3hny5h82wb3.jpg", Description = "The perfect item for you", Supplier = supplier},
                };

            foreach (var item in items)
            {
                item.ItemCategories.Add(new ItemCategory { ChildCategory = fitnessCategory, Item = item });
                item.ItemCategories.Add(new ItemCategory { ChildCategory = clothingCategory, Item = item });
                item.ItemCategories.Add(new ItemCategory { ChildCategory = supplementsCategory, Item = item });
            }

            await this.context.Items.AddRangeAsync(items);
            await this.context.SaveChangesAsync();
        }
    }
}