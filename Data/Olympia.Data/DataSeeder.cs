namespace Olympia.Data.Seeding
{
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using System.Collections.Generic;

    using System.Linq;

    public class DataSeeder
    {
        private readonly OlympiaDbContext context;

        public DataSeeder(OlympiaDbContext context)
        {
            this.context = context;

            this.SeedRoles();
            this.SeedWorkoutAndExercises();
            this.SeedCategories();
            this.SeedArticles();
            this.SeedSuppliers();
            this.SeedItems();

        }

        private void SeedRoles()
        {
            if (!this.context.Roles.Any())
            {
                this.context.Roles.Add(new OlympiaUserRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
                this.context.Roles.Add(new OlympiaUserRole { Name = "Trainer", NormalizedName = "TRAINER" });
                this.context.Roles.Add(new OlympiaUserRole { Name = "Client", NormalizedName = "CLIENT" });

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

                this.context.SaveChanges();
            }
        }

        private void SeedWorkoutAndExercises()
        {
            if (!this.context.Exercises.Any())
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


                this.context.Workouts.AddRange(strengthWokouts);
                this.context.Workouts.AddRange(circuitWorkouts);
                this.context.Workouts.AddRange(crossfitWorkouts);
                this.context.Workouts.AddRange(cardioWorkouts);
                this.context.Workouts.AddRange(hiitWorkouts);
                this.context.SaveChanges();
            }
        }

        private void SeedCategories()
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

        private void SeedArticles()
        {
            if (!this.context.Articles.Any())
            {
                var user = this.context.Users.SingleOrDefault(x => x.UserName == "Pesho");
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

                this.context.Articles.AddRange(articles);
                this.context.SaveChanges();
            }
        }

        private void SeedSuppliers()
        {
            if (!this.context.Suppliers.Any())
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

                this.context.Suppliers.AddRange(suppliers);
                this.context.SaveChanges();
            }
        }

        private void SeedItems()
        {
            if (!this.context.Items.Any())
            {
                var fitnessCategory = this.context.ChildCategories.SingleOrDefault(cat => cat.Name == "Fitness");
                var clothingCategory = this.context.ChildCategories.SingleOrDefault(cat => cat.Name == "Clothing");
                var supplementsCategory = this.context.ChildCategories.SingleOrDefault(cat => cat.Name == "Supplements");

                var supplier = this.context.Suppliers.SingleOrDefault(supp => supp.Name == "GymBeam");

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

                this.context.Items.AddRange(items);
                this.context.SaveChanges();
            }
        }
    }
}