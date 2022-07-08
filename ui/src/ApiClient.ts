function getBaseUrl() {
    let b = (process.env.REACT_APP_API_BASE_URL ?? '/').trim()
    if(!b.endsWith('/')) {
        b += '/'
    }
    return b
}

export function getAbsoluteApiUrl(relativeUrl: string) {
    let b = getBaseUrl()
    return relativeUrl.startsWith('/')
        ? b + relativeUrl.substring(1)
        : b + relativeUrl
}

export function fetchApi(input: RequestInfo | URL, init?: RequestInit): Promise<Response> {
    if(input instanceof Request) {
        input = { ...input, url: getAbsoluteApiUrl(input.url) }
    } else if(input instanceof URL)  {
        //Assumed to be absolute already
    } else { //string
        input = getAbsoluteApiUrl(input)
    }
    return fetch(input, init)
}