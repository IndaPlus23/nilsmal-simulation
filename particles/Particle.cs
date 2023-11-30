using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace particles {

    public class Particle
    {
        private Random random;

        private double[] initialLocation;
        private double initialSpeedConstant;
        private double[] initialAcceleration;
        private int initialLifespan;

        public double[] Location { get; private set; }
        public double[] Velocity { get; private set; }
        public double[] Acceleration { get; private set; }
        public int Lifespan { get; set; }

        public double RotationAngle { get; private set; } // Make rotationAngle accessible

        public Particle(double[] initialLocation, double initialSpeedConstant, double[] initialAcceleration, int lifespan)
        {
            random = new Random();
            this.initialLocation = initialLocation;
            this.initialSpeedConstant = initialSpeedConstant;
            this.initialAcceleration = initialAcceleration;
            this.initialLifespan = lifespan;
            RotationAngle = 0;

            Reset(); // Initialize the particle with the initial values
        }

        public void Reset()
        {
            Random random = new Random();

            // Generate random angle in radians
            double randomAngle = random.NextDouble() * 2 * Math.PI;

            // Generate random speed
            double randomSpeed = random.NextDouble() * initialSpeedConstant;

            // Calculate initial velocity components using polar coordinates
            double randomX = randomSpeed * Math.Cos(randomAngle);
            double randomY = randomSpeed * Math.Sin(randomAngle);

            // Reset particle state to initial values
            Location = (double[])initialLocation.Clone();
            Velocity = new double[] { randomX, randomY };
            Acceleration = (double[])initialAcceleration.Clone();
            Lifespan = initialLifespan;
            RotationAngle = 0;
        }

        public void Update(double timeStep)
        {
            // Update velocity based on acceleration
            for (int i = 0; i < 2; i++)
            {
                Velocity[i] += Acceleration[i] * timeStep * 10;
            }

            // Update location based on velocity
            for (int i = 0; i < 2; i++)
            {
                Location[i] += Velocity[i] * timeStep * 5;
            }

            Lifespan -= 1;

            if (Lifespan <= 0)
            {
                Reset();
            }

            RotationAngle += Math.PI * timeStep;
        }

        // Method to print particle state
        public void PrintState()
        {
            Console.WriteLine($"Location: ({Location[0]}, {Location[1]})");
            Console.WriteLine($"Velocity: ({Velocity[0]}, {Velocity[1]})");
            Console.WriteLine($"Acceleration: ({Acceleration[0]}, {Acceleration[1]})");
        }
    }
}