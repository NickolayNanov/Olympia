namespace Olympia.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Olympia.Common;
    using Olympia.Data.Domain;
    using Olympia.Data.Domain.Enums;
    using System.Collections.Generic;

    using System.Linq;

    public class RealDataSeeder
    {
        private readonly OlympiaDbContext context;
        private readonly UserManager<OlympiaUser> userManager;

        public RealDataSeeder(OlympiaDbContext context, UserManager<OlympiaUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.SeedRoles();
            this.SeedUser();
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

                this.context.SaveChanges();
            }
        }

        private void SeedUser()
        {
            if (!this.context.Users.Any())
            {
                var user = new OlympiaUser("NikolayNanov",
                                           "nickolaynanov17@gmail.com",
                                           "Nickolay Borislavov Nanov")
                {
                    ProfilePicturImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565266354/egjjjw4a8qd1ugwnam2o.jpg"
                };

                var result = this.userManager.CreateAsync(user).Result;

                if (result.Succeeded)
                {
                    this.userManager.AddToRoleAsync(user, GlobalConstants.TrainerRoleName);
                }
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
                var user = this.context.Users.SingleOrDefault(x => x.UserName == "NikolayNanov");
                var articles = new List<Article>()
                {
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "Start Here: The Most Important Supplements For Every Body ",
                        Content = @"You might cringe when you hear nails across a chalkboard—if you can still find a chalkboard, anyway. Me, I cringe when a nearby conversation begins with, What supplement should I take to [fill in the goal]?" +
                                    "If you are in the health and fitness industry, or just look like you're relatively healthy and fit, you've probably been asked the same thing. There are just too many confused people, wanting to believe the answer lies just one pill or powder away. And there are just as many bad shepherds feeding that misbelief, too. Let's fix that!" +
                                    "In one way or another, I've walked countless thousands of people through what you're about to read. This is my keep it simple and smart approach to effective supplementation. I'll give you six steps to help you get the most out of whatever supplements you do choose to take, and then my six recommendations for a supplement starter set.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565266484/bpzhl5fxzqducujc8au5.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "The 10 Best Single-Leg Exercises You've Probably Never Tried ",
                        Content = @"You go to the gym regularly. You do your squats and deadlifts, maybe some stiff-legged deadlifts and leg extensions every now and then. You know that developing your lower body is as important as developing your upper body. Loading up the barbell truly gives you joy; you love getting stronger and watching your quadriceps, hamstrings, and glutes grow.
                                    Occasionally, you throw in some Bulgarian split squats or lunges, but here's the thing—you're leaving a lot of strength and size gains on the table by not incorporating single-leg exercises more often.
                                    No, you can't lift as much weight during single-leg exercises as you would with bilateral exercises, meaning with both legs, but you can take your muscle growth and symmetry to the next level with single-leg moves.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565266958/r00v7s8pxg4aqgfcljs2.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "5 Keys to a Bigger Bench Press ",
                        Content = @"Regardless of how experienced a lifter you are, you should be reanalyzing the basics constantly, particularly on staple exercises like the bench press. It's one of the best strength and size builders for the upper body, so doing it correctly is crucial.
                                    One of the biggest misconceptions about the bench press is that it is a dangerous exercise. Sure, if you do it improperly, you can get hurt, but you can get hurt crossing the street if you don't pay attention to the crosswalk lights and oncoming traffic.
                                    The bench press is no more dangerous than any other upper-body pressing exercise you do with dumbbells or even a machine.There are ways to do things in a dangerous manner, and there are ways to do them safely.
                                    When it comes to bench pressing for size and strength and without injury, follow the list below, and you'll be positioned to make great upper-body gains, minus the injuries.",

                       ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565266886/vwqkwpmg4x5y9oqvhgyg.jpg"
                    },
                    new Article
                    {
                        AuthorId = user.Id,
                        Title = "4 BCAA Craft Cocktails to Shake Up Your Summer ",
                        Content = @"When you commit to the fitness lifestyle, sugary cocktails are one of the first things to go. So much for happy hour margaritas and rooftop cocktails—they're not anabolic. Not to worry, though. Your days of ordering vodka sodas and gazing longingly at your friends' craft cocktails are over. Mixologist Terance Robson of the speakeasy Here Nor There in Austin, Texas, voted Best Bartender in Texas, is here to save your palate with a menu of four complex, grown-up cocktails made with XTEND BCAAs.",
                        ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565266734/gyiqwqrlgqviiaiystvv.jpg"
                    },
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

                var itemsSupplements = new List<Item>()
                {
                    new Item("Protein Crisp ", 21.24m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267391/ndzwwcxbus6gzdyzfb7k.jpg", Description = @"Crispy Protein Bar Provides 20 Grams Of Protein With 4 Grams Of Sugar Per Bar. Bars Have A Unique Rice Crispy Like Texture", Supplier = supplier},
                    new Item("100% Gold Standard Whey Protein Powder ", 29.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267580/owjr93ybaiekhunev7om.jpg", Description = @"24g of Whey Protein with Amino Acids for Muscle Recovery and Growth*Muscle Building Whey Protein Powder*", Supplier = supplier},
                    new Item("Protein Powder", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267333/g1z7aijujdmmabxenhaz.jpg", Description = "The perfect item for you", Supplier = supplier},
                };

                var itemsFitness = new List<Item>()
                {
                    new Item("Competition Power Belt ", 69.95m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267671/uwjdhomsuesil5vvxl1k.jpg", Description = @"Model 6010 Super Heavy Duty Belt Perfect For Power Lifters!", Supplier = supplier},
                    new Item("Expedition Backpack 6 Pack Bag , 3 Meal - Small Black/Black", 170.00m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267726/oyuhyne9w75mxnuc2bky.jpg", Description = @"Durable, Insulated Backpack For Endurance In And Out Of The Gym!Offers Comprehensive Meal Management Capabilities While Storing Gear For The Gym And Technology For The Office!", Supplier = supplier},
                    new Item("Assassin Elbow ", 50.00m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267802/izzrls04ocpe5dbbdy9f.jpg", Description = @"Provide Joint Support During Grueling Workouts And Lifts Made With Sbr Neoprene And Double Stitching For Durability, With Flexible Front Panels And Lycra Side Panels", Supplier = supplier},
                };

                var itemsClothing = new List<Item>()
                {
                    new Item("Est. 1999 Campus Tee ", 20.00m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565267865/sep0milo9sn5n0cxu0za.jpg", Description = @"60% Combed Ring-Spun Cotton, 40% Polyester Tee Breathable, Fitted Cut Supports Intense Exercise Without the Weight of Heavier Fabrics", Supplier = supplier},
                    new Item("Est. 1999 B Swoosh Mesh Back Trucker Hat , Black One Size - Adjustable", 19.99m){ ImgUrl = "https://res.cloudinary.com/olympiacloudinary/image/upload/v1565268260/rxv1qhgdeiev3q7ep9mt.jpg", Description = "The perfect item for you", Supplier = supplier},
                };

                foreach (var item in itemsSupplements)
                {
                    this.context.ItemCategories.Add(new ItemCategory() { Item = item, ChildCategory = supplementsCategory });
                }

                foreach (var item in itemsFitness)
                {
                    this.context.ItemCategories.Add(new ItemCategory() { Item = item, ChildCategory = fitnessCategory });
                }

                foreach (var item in itemsClothing)
                {
                    this.context.ItemCategories.Add(new ItemCategory() { Item = item, ChildCategory = clothingCategory });
                }

                this.context.Items.AddRange(itemsSupplements);
                this.context.Items.AddRange(itemsFitness);
                this.context.Items.AddRange(itemsClothing);
                this.context.SaveChanges();
            }
        }
    }
}