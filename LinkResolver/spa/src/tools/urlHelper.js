export const urlRegexPattern = "(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";

export const shortUrlRegexPatter = `^http:\/\/myhost.ru\/[A-Za-z0-9]{6}$`;

export const isURL = (url) => {
    const regex = new RegExp(urlRegexPattern);
    return regex.test(url);
}

export const placeHolders = {
    httpUrl: "http://www.example.com",
    shortLink: "http://myhost.ru/abc123",
    shortLinkBaseURL: "http://myhost.ru"
}

export const isShortURL = (url) => {
    const regex = new RegExp(shortUrlRegexPatter);
    return regex.test(url);
}