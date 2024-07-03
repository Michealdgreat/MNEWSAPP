# MNewsApp

MNewsApp is a Cross-Platform mobile application built with .NET MAUI that provides users with the latest news articles from Newsapi.org. It utilizes the News API to fetch and display news articles based on keywords and other criteria. The app also handles different scenarios such as API key management and connection errors.

## Features

- Fetches news articles from the Newsapi.org
- Displays articles with images, titles, authors, and publication dates
- Allows users to read full articles by navigating to a web view
- Handles API key management and prompts users to set an API key if not provided
- Manages connection errors and displays appropriate error messages
- Implements MVVM architecture
- Supports font size adjustments

## Screenshots
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(4).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(13).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(6).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(14).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(8).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(16).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(3).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(17).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(15).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(7).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(10).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(5).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(17).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(9).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(12).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(11).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(2).png" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(1).jpg" alt="screenshot" width="200"/>
<img src="MNEWSAPP/ReadMe/mnews_screenshot%20(4).png" alt="screenshot" width="200"/>





## Architecture

MNEWSAPP follows the MVVM (Model-View-ViewModel) architecture pattern:
- **Models**: Represent the data structures (e.g., `ArticleModel`)
- **Views**: Define the UI (e.g., `ArticleDetailsView`, `ChangeFontSizeView`)
- **ViewModels**: Handle the presentation logic and data binding (e.g., `HomeViewModel`, `FontSizeViewModel`)

## Dependency Injection

The project uses dependency injection to manage dependencies. Services are configured and registered with the dependency injection container, ensuring that dependencies are easily manageable and promoting a modular architecture.

## Error Handling

The application handles different types of errors, such as API key issues and connection errors. When an error occurs, the application navigates to a specific error view and displays an appropriate message, enhancing the user experience by providing clear feedback on issues.

## Contributing

Contributions are welcome! Please follow these steps:
1. Fork the repository
2. Create a new branch 
3. Make your changes
4. Commit your changes 
5. Push to the branch 
6. Create a new Pull Request

## License

This project is licensed under the MIT License 😊.
