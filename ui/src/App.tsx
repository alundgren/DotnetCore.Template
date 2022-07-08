import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import { fetchApi } from "./ApiClient";

interface Forecast {
  date: string,
  temperatureC: number,
  summary: string,
  temperatureF: number
}
function App() {
  let [foreCasts, setForecasts] = useState<Forecast[] | null>(null)

  useEffect(() => {
    fetchApi('api/weatherforecast').then(x => {
      let result = x.json() as Promise<{
        date: string,
        temperatureC: number,
        summary: string,
        temperatureF: number
      }[]>
      result.then(y => setForecasts(y))
    })
  }, []);
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        {foreCasts?.map(x => <div key={x.summary}>{x.summary}</div>)}
      </header>
    </div>
  );
}

export default App;
