'use client'
import styles from "./page.module.css";
import { RepoInput } from "./pullRequest";

export async function fetchData() {
  const prNumber = -1;
  const repoName = "msirkovsky-moodys/PullRequestReviewTest";
  const personalToken = "";
  const prompt = ` My C# code:
{code}
---
Improve it to conform these rules:
var plus value type should be write as const value type.
And also add anything you think it's worth improving.
`;

  return {
    prNumber,
    repoName,
    personalToken,
    prompt
  };
}

export default async function Home() {

  const data = await fetchData();
  return (
    <main className={styles.main}>
      <div className={styles.description}>
        <h1>
          SageReview v0.1
        </h1>
        <div>          
        </div>
      </div>

      <div className={styles.center}>
        <RepoInput {...data} />
      </div>

    
    </main>
  );
}
