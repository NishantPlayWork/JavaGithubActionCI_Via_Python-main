import subprocess
import sys

def run_command(command, args):
    print(f"Running: {command} {' '.join(args)}")
    try:
        result = subprocess.run(
            [command] + args,
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True,
            check=True
        )
        print(result.stdout)
        if result.stderr:
            print(f"Error: {result.stderr}")
    except subprocess.CalledProcessError as e:
        print(f"Pipeline failed: {e.stderr}")
        sys.exit(1)

def main():
    print("Starting CI Pipeline...")

    # Step 1: Build the Project
    print("Building the project...")
    run_command("mvn", ["clean", "install"])

    # Step 2: Run Tests
    print("Running tests...")
    run_command("mvn", ["test"])

    print("CI Pipeline completed successfully!")

if __name__ == "__main__":
    main()
